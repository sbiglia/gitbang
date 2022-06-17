using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Gitbang.Core.Base;
using Gitbang.Core.Models;

namespace Gitbang.Core
{
    public sealed class ThemeManager : ModelBase
    {
        private static readonly object _syncLock;
        private static ThemeManager _instance;
        private readonly List<ThemeModel> _themes;

        static ThemeManager()
        {
            _syncLock = new object();
        }

        private ThemeManager()
        {
            _themes = new List<ThemeModel>
            {
                new ThemeModel("LightTheme",
                    new Uri("avares://Avalonia.Themes.Default/Accents/BaseLight.xaml"),
                    new Uri("avares://Jaya.Shared/Styles/Accents/BaseLight.xaml")),
                new ThemeModel("DarkTheme",
                    new Uri("avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"),
                    new Uri("avares://Jaya.Shared/Styles/Accents/BaseDark.xaml"))
            };

            SelectedTheme = _themes[0];
        }


        public static ThemeManager Instance
        {
            get
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                        _instance = new ThemeManager();

                    return _instance;
                }
            }
        }

        public IEnumerable<ThemeModel> Themes => _themes;

        public ThemeModel SelectedTheme
        {
            get => Get<ThemeModel>();
            set
            {
                if (Design.IsDesignMode)
                    return;

                var currentTheme = Get<ThemeModel>();

                if (!Set(value) || value.Styles.Count == 0)
                    return;

                var currentAppStyles = new List<IStyle>();
                currentAppStyles.AddRange(Application.Current.Styles);

                // remove default theme styles
                currentAppStyles.RemoveRange(0, 2);

                currentAppStyles.InsertRange(0, SelectedTheme.Styles);

                Application.Current.Styles.Clear();
                Application.Current.Styles.AddRange(currentAppStyles);

                if (currentTheme != null)
                {
                    /*foreach (var window in _windows)
                    {*/
                    // foreach (var style in currentTheme.Styles)
                    //    Application.Current.Styles.Remove(style);

                    //foreach (var style in SelectedTheme.Styles)
                    //   Application.Current.Styles.Add(style);
                    //}
                }
            }
        }

        public void EnableTheme(Window window)
        {
            if (Design.IsDesignMode)
            {
                if (SelectedTheme != null && SelectedTheme.Styles.Count > 0)
                    foreach (var style in SelectedTheme.Styles)
                        window.Styles.Add(style);
            }

            /*window.Opened += (sender, e) =>
            {
                _windows.Add(window);

                if (SelectedTheme != null && SelectedTheme.Styles.Count > 0)
                    foreach (var style in SelectedTheme.Styles)
                        window.Styles.Add(style);
            };

            window.Closing += (sender, e) =>
            {
                _windows.Remove(window);
            };*/
        }
    }
}
