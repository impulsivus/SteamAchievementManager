﻿using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace SAM.WPF.Core.Cache
{
    public static class CacheManager
    {

        public static void CacheObject(ICacheKey key, object target, bool overwrite = true)
        {
            var filePath = key?.GetFullPath();
            
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));

            var targetObjectJson = JsonConvert.SerializeObject(target);

            IsolatedStorageManager.SaveText(filePath, targetObjectJson, overwrite);
        }

        public static void CacheText(ICacheKey key, string text, bool overwrite = true)
        {
            var filePath = key?.GetFullPath();
            
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));

            IsolatedStorageManager.SaveText(filePath, text, overwrite);
        }

        public static void CacheImage(ICacheKey key, Image img, bool overwrite = true)
        {
            var filePath = key?.GetFullPath();
            
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));

            IsolatedStorageManager.SaveImage(filePath, img, overwrite);
        }

        public static bool TryGetImageFile(ICacheKey key, out Image img)
        {
            var filePath = key?.GetFullPath();

            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));
            
            if (!IsolatedStorageManager.FileExists(filePath))
            {
                img = null;
                return false;
            }

            img = IsolatedStorageManager.GetImageFile(filePath);

            return true;
        }

        public static Image GetImageFile(ICacheKey key)
        {
            var filePath = key.GetFullPath();

            if (!IsolatedStorageManager.FileExists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            return IsolatedStorageManager.GetImageFile(filePath);
        }

        public static bool TryGetObject<T>(ICacheKey key, out T cachedObject)
        {
            var filePath = key?.GetFullPath();

            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));
            
            if (!IsolatedStorageManager.FileExists(filePath))
            {
                cachedObject = default;
                return false;
            }

            var fileText = IsolatedStorageManager.GetTextFile(filePath);

            cachedObject = JsonConvert.DeserializeObject<T>(fileText);

            return true;
        }


        public static bool TryGetTextFile(ICacheKey key, out string fileText)
        {
            var filePath = key?.GetFullPath();

            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(key));
            
            if (!IsolatedStorageManager.FileExists(filePath))
            {
                fileText = null;
                return false;
            }

            fileText = IsolatedStorageManager.GetTextFile(filePath);

            return true;
        }

        public static string GetTextFile(ICacheKey key)
        {
            var filePath = key.GetFullPath();

            if (!IsolatedStorageManager.FileExists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            return IsolatedStorageManager.GetTextFile(filePath);
        }

    }
}