using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gitbang.Core.Settings
{
    public class ConfigurationManager
    {
        readonly string _configurationFilePathFormat;

        public ConfigurationManager(string configurationDirectory)
        {
            ConfigurationDirectory = configurationDirectory;
            _configurationFilePathFormat = Path.Combine(ConfigurationDirectory, "{0}.json");
        }

        ~ConfigurationManager()
        {

        }

        public string ConfigurationDirectory { get; }

        public T Get<T>(string key = null) where T : JsonSettings
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);

            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
            if (fileInfo.Exists)
            {
                using (var reader = File.OpenText(fileInfo.FullName))
                {
                    var serializer = new JsonSerializer { Formatting = Formatting.None };
                    return serializer.Deserialize(reader, type) as T;
                }
            }
            else
                return default;
        }

        public T GetOrDefault<T>(string key = null) where T : JsonSettings
        {
            var config = Get<T>(key) ?? JsonSettings.Empty<T>();

            return config;
        }

        public void Set<T>(T value, string key = null)
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);

            // create configuration directory if missing
            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);

            using (var writer = File.CreateText(fileInfo.FullName))
            {
                var serializer = new JsonSerializer { Formatting = Formatting.None };
                serializer.Serialize(writer, value, type);
            }
        }

        string GetUsableKey(Type type)
        {
            var name = type.Name;

            var invalidChars = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var invalidChar in invalidChars)
                name = name.Replace(invalidChar, '_');

            return name;
        }


    }
}
