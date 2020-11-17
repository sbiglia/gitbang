using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gitbang.Views
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