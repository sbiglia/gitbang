using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Gitbang.Themes.Macos
{
    public class SharedStyles : Styles
    {
        public SharedStyles()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}