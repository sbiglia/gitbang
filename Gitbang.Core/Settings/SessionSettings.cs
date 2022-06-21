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

namespace Gitbang.Core.Settings
{
    /// <summary>
    /// Per-session setting:
    /// Loaded at startup; saved at exit.
    /// </summary>
    public sealed class SessionSettings : INotifyPropertyChanged
    {
        public SessionSettings(GitBangSettings appSettings)
        {
            XElement doc = appSettings["SessionSettings"];
            
            this.WindowState = FromString((string)doc.Element("WindowState"), WindowState.Normal);
            this.WindowBounds = FromString((string)doc.Element("WindowBounds"), DefaultWindowBounds);
            this.SplitterPosition = FromString((string)doc.Element("SplitterPosition"), 0.4);
            this.TopPaneSplitterPosition = FromString((string)doc.Element("TopPaneSplitterPosition"), 0.3);
            this.BottomPaneSplitterPosition = FromString((string)doc.Element("BottomPaneSplitterPosition"), 0.3);
            this.Theme = (string)doc.Element("Theme");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public WindowState WindowState = WindowState.Normal;
        public Rect WindowBounds;
        internal static Rect DefaultWindowBounds = new Rect(10, 10, 750, 550);

        /// <summary>
        /// position of the left/right splitter
        /// </summary>
        public double SplitterPosition;

        public double TopPaneSplitterPosition, BottomPaneSplitterPosition;
        public string Theme;

        public void Save()
        {
            var doc = new XElement("SessionSettings");
        
            doc.Add(new XElement("WindowState", ToString(this.WindowState)));
            doc.Add(new XElement("WindowBounds", ToString(this.WindowBounds)));
            doc.Add(new XElement("SplitterPosition", ToString(this.SplitterPosition)));
            doc.Add(new XElement("TopPaneSplitterPosition", ToString(this.TopPaneSplitterPosition)));
            doc.Add(new XElement("BottomPaneSplitterPosition", ToString(this.BottomPaneSplitterPosition)));
            
            doc.Add(new XElement("Theme", this.Theme));

            GitBangSettings.SaveSettings(doc);
        }

        static readonly Regex Regex = new Regex("\\\\x(?<num>[0-9A-f]{4})");

        static string Escape(string p)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in p)
            {
                if (char.IsLetterOrDigit(ch))
                    sb.Append(ch);
                else
                    sb.AppendFormat("\\x{0:X4}", (int)ch);
            }

            return sb.ToString();
        }

        private static string Unescape(string p)
        {
            return Regex.Replace(p, m => ((char)int.Parse(m.Groups["num"].Value, NumberStyles.HexNumber)).ToString());
        }

        static T FromString<T>(string s, T defaultValue)
        {
            if (s == null)
                return defaultValue;
            try
            {
                var c = TypeDescriptor.GetConverter(typeof(T));
                return (T)c.ConvertFromInvariantString(s);
            }
            catch (FormatException)
            {
                return defaultValue;
            }
        }

        private static Rect FromString(string s, Rect defaultValue)
        {
            if (s == null)
                return defaultValue;
            return Rect.Parse(s);
        }

        private static string ToString<T>(T obj)
        {
            var c = TypeDescriptor.GetConverter(typeof(T));
            return c.ConvertToInvariantString(obj);
        }
    }
}

