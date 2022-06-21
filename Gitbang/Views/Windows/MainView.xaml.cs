using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using Gitbang.Core.Controls;
using Gitbang.Core.Settings;


namespace Gitbang.Views.Windows
{
    public partial class MainView : PlatformDependentWindow
    {
        private static SessionSettings _sessionSettings;

        public static SessionSettings SessionSettings => _sessionSettings;

        private GitBangSettings _appSettings;

        public MainView()
        {
            _appSettings = GitBangSettings.Load();
            _sessionSettings = new SessionSettings(_appSettings);
            InitializeComponents();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        private void InitializeComponents()
        {
            AvaloniaXamlLoader.Load(this);
            
            //Just to avoid crashing Avalonia's Designer
            if (Design.IsDesignMode)
            {
                return;
            }

            var themesNames = new List<string>();
            var themes = new List<IStyle>();

            foreach (var file in Directory.EnumerateFiles("Themes", "*.xaml"))
            {
                try
                {
                    var theme = AvaloniaRuntimeXamlLoader.Parse<Styles>(File.ReadAllText(file));
                    themes.Add(theme);
                    themesNames.Add(Path.GetFileNameWithoutExtension(file));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, String.Format(Gitbang.Core.Properties.Resources.UnableLoadTheme, file));
                }
            }

            if (themes.Count == 0)
            {
                var light = AvaloniaRuntimeXamlLoader.Parse<StyleInclude>(
                    @"<StyleInclude xmlns='https://github.com/avaloniaui' Source='resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default'/>");
                themes.Add(light);
                themesNames.Add(Gitbang.Core.Properties.Resources.LightTheme);


                var dark = AvaloniaRuntimeXamlLoader.Parse<StyleInclude>(
                    @"<StyleInclude xmlns='https://github.com/avaloniaui' Source='resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default'/>");
                themes.Add(dark);
                themesNames.Add(Gitbang.Core.Properties.Resources.DarkTheme);
            }


            var themesDropDown = this.Find<ComboBox>("Themes");
            themesDropDown.Items = themesNames;
            themesDropDown.SelectionChanged += (sender, e) =>
            {
                Styles[0] = themes[themesDropDown.SelectedIndex];
                _sessionSettings.Theme = themesNames[themesDropDown.SelectedIndex];
                ApplyTheme();
            };

            Styles.Add(themes[0]);
            int selectedTheme = themesNames.IndexOf(_sessionSettings.Theme);
            themesDropDown.SelectedIndex = selectedTheme < 0 ? 0 : selectedTheme;
        }

		private void ApplyTheme()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && Styles.TryGetResource("ThemeBackgroundBrush", out object backgroundColor) && backgroundColor is ISolidColorBrush brush)
			{
				// HACK: SetTitleBarColor is a method in Avalonia.Native.WindowImpl
				var setTitleBarColorMethod = PlatformImpl.GetType().GetMethod("SetTitleBarColor");
				setTitleBarColorMethod?.Invoke(PlatformImpl, new object[] { brush.Color });
			}

			
			//Reload text editor
			//DecompileSelectedNodes();
		}

	}
}