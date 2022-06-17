using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gitbang_old.Views.ReposExplorer
{
    public class ReposTreeView : UserControl
    {
        public ReposTreeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}