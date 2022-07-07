using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gitbang.Core.Helpers;
using Gitbang.Core.Settings.Interfaces;

namespace Gitbang.Core.Settings
{
    public class SettingsManager : ISettingsManager
    {

        private const string ConfigMutex = "355BE18F-FE69-437F-B8D2-7F085D975457";

        private readonly string _configurationFilePathFormat;

        public SettingsManager(string configurationDirectory)
        {
            ConfigurationDirectory = configurationDirectory;
            _configurationFilePathFormat = Path.Combine(ConfigurationDirectory, "{0}.json");
        }

        ~SettingsManager()
        {
            SaveConfiguration();
        }

        public string ConfigurationDirectory { get; }

        public T Load<T>(string key = null) where T : JsonSettings
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);

            using var mutex = new MutexProtector(ConfigMutex);

            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
           
            if (fileInfo.Exists)
            {
                using var reader = File.OpenText(fileInfo.FullName);
                var serializer = new JsonSerializer { Formatting = Formatting.None };
                return serializer.Deserialize(reader, type) as T;
            }
            else
                return default;
        }

        private T LoadOrDefault<T>(string key = null) where T : JsonSettings
        {
            var config = Load<T>(key) ?? JsonSettings.Empty<T>();

            return config;
        }

        private void Save<T>(T value, string key = null)
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);


            var mutex = new MutexProtector(ConfigMutex);

            // create configuration directory if missing
            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
            
            if (fileInfo.Directory is { Exists: false })
                if (fileInfo.DirectoryName != null)
                    Directory.CreateDirectory(fileInfo.DirectoryName);

            using var writer = File.CreateText(fileInfo.FullName);

            var serializer = new JsonSerializer { Formatting = Formatting.None };
            serializer.Serialize(writer, value, type);
        }

        private string GetUsableKey(Type type)
        {
            var name = type.Name;

            var invalidChars = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var invalidChar in invalidChars)
                name = name.Replace(invalidChar, '_');

            return name;
        }

        private SessionSettings _sessionSettings;

        public ISessionSettings SessionSettings
        {
            get
            {
                if (_sessionSettings == null)
                {
                    _sessionSettings = LoadOrDefault<SessionSettings>();
                } 
                
                return _sessionSettings;
            }
        }
        
        public void SaveConfiguration()
        {
            Save(_sessionSettings);
        }
    }
}
