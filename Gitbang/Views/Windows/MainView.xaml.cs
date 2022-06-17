using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Gitbang.Core.Controls;


namespace Gitbang.Views.Windows
{
    public partial class MainView : PlatformDependentWindow
    {

        public MainView()
        {
            InitializeComponents();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        private void InitializeComponents()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}