using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang_old.Themes.Base
{
    public class IconStyles : Styles
    {
        public IconStyles()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
