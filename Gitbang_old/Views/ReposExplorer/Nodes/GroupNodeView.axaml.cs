using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gitbang_old.Views.ReposExplorer.Nodes
{
    public class GroupNodeView : UserControl
    {
        public GroupNodeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}