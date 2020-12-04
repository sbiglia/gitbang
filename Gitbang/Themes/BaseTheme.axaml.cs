using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gitbang.Themes
{
    public class BaseTheme : UserControl
    {
        public BaseTheme()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
