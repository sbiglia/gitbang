using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang_old.Themes.Windows
{
    public class SharedStyles : Styles
    {
        public SharedStyles()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}