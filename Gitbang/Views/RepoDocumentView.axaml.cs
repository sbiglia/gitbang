using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gitbang.Views
{
    public class RepoDocumentView : UserControl
    {
        public RepoDocumentView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
