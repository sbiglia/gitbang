using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gitbang.Services;
using Gitbang.Services.ExecuteCommands;
using Gitbang.Services.Queries;
using Gitbang.ViewModels;
using Gitbang.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Gitbang
{
    public class App : Application
    {

        private IConfiguration _configuration;
        private IExecuter _executer;
        private ICommandText _commandText;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                                 .AddJsonFile("appsettings.json", optional: true);

                _configuration = builder.Build();

                _commandText = new CommandText();
                _executer = new Executer();

                var repo = new DataRepository(_configuration, _commandText, _executer);
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(repo),
                };

                var theme = new Avalonia.Themes.Default.DefaultTheme();
                theme.TryGetResource("Button", out _);

                /*var theme1 = new Avalonia.Themes.Fluent.FluentTheme();
                theme1.TryGetResource("Button", out _);*/
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}