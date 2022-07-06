﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gitbang.Core.Extensions;
using Gitbang.Core.Services;
using Gitbang.Core.Settings;
using Gitbang.Core.Settings.Interfaces;
using Gitbang.ViewModels;
using Gitbang.Views;
using Splat;


namespace Gitbang
{
    public partial class App : Application
    {
        public static IConfigurationManager? ConfigurationManager { get; private set; }

        public App()
        {
            Name = "Gitbang";
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ConfigurationManager = new ConfigurationManager(Locator.Current.GetService<IEnvironmentService>().GetApplicationDataDirectory());
        }

        public override void OnFrameworkInitializationCompleted()
        {

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (Lifetime != null) Lifetime.Exit += OnExit;

                desktop.MainWindow = new MainView()
                {
                    DataContext = new MainViewViewModel()
                };
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            ConfigurationManager?.SaveConfiguration();
        }

        internal static IClassicDesktopStyleApplicationLifetime? Lifetime => Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

    }
}