using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Newtonsoft.Json;

namespace Gitbang.Core.Settings
{
    /// <summary>
    /// Per-session setting:
    /// Loaded at startup; saved at exit.
    /// </summary>
    public sealed class SessionSettings : JsonSettings
    {

        public SessionSettings()
        {
        }
        
        [JsonProperty]
        public WindowState WindowState
        {
            get => Get<WindowState>();
            set => Set(value);
        }
        
        [JsonProperty]
        public float SplitterPosition
        {
            get => Get<float>();
            set => Set(value);
        }

        [JsonProperty]
        public float TopPaneSplitterPosition
        {
            get => Get<float>();
            set => Set(value);
        }

        [JsonProperty]
        public float BottomPaneSplitterPosition
        {
            get => Get<float>();
            set => Set(value);
        }

        [JsonProperty]
        public string Theme
        {
            get => Get<string>();
            set => Set(value);
        }

        protected override JsonSettings Empty()
        {
            return new SessionSettings
            {
                WindowState = WindowState.Normal,
                SplitterPosition = 0.4f,
                TopPaneSplitterPosition = 0.3f,
                BottomPaneSplitterPosition = 0.3f,
                Theme = "Light"
            };
        }
    }
}

