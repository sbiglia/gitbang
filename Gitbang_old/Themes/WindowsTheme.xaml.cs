using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang_old.Themes
{
    public class WindowsTheme : Styles
    {
        public WindowsTheme()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
