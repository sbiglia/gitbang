using Avalonia.Controls;
using Avalonia.Markup.Xaml;


namespace Gitbang.Views.Views
{
    public partial class RepoView : UserControl
    {
        public RepoView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}