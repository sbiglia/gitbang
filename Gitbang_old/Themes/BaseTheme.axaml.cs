using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang_old.Themes
{
    public class BaseTheme : Styles
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
