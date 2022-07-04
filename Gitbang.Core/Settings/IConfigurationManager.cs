using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.Settings
{
    public interface IConfigurationManager
    {
        string ConfigurationDirectory { get; }

        T Get<T>(string key = null) where T : JsonSettings;

        T GetOrDefault<T>(string key = null) where T : JsonSettings;

        void Set<T>(T value, string key = null);
    }
}
