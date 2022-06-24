using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gitbang.Core.Extensions;
using Gitbang.Views;


namespace Gitbang
{
    public partial class App : Application
    {
        public App()
        {
            Name = "Gitbang";
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                desktop.MainWindow = new MainView();

            }
            base.OnFrameworkInitializationCompleted();
        }

        internal static IClassicDesktopStyleApplicationLifetime Lifetime => Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

    }
}