using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang.Themes
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
