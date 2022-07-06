using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.Logging;
using Gitbang.DependencyInjection;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Projektanker.Icons.Avalonia.MaterialDesign;
using Serilog;
using Splat;

namespace Gitbang
{
    static class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            
            try
            {
                using var log = new LoggerConfiguration()
                    .WriteTo.Debug()
                    .WriteTo.File(Constants.LogFileName)
                    .CreateLogger();

                SuscribeToDomainUnhandledEvents();
                RegisterDependencies();

                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            }
            catch (Exception exception)
            {
                Console.WriteLine("Sorry, we crashed");
                Console.WriteLine(exception.ToString());
            }

        }

        public static AppBuilder BuildAvaloniaApp()
        {
            var result = AppBuilder.Configure<App>();

#if DEBUG
            result.LogToTrace();

#endif
            var app = result
                .UsePlatformDetect()
                .WithIcons(container => container
                    .Register<FontAwesomeIconProvider>()
                    .Register<MaterialDesignIconProvider>())
                .With(new X11PlatformOptions
                {
                    UseDBusMenu = true
                });



            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                app = app.UseManagedSystemDialogs();
            }

            return app;
        }

        private static void RegisterDependencies() => Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);

        private static void SuscribeToDomainUnhandledEvents() =>
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var ex = (Exception)args.ExceptionObject;
                Log.Logger.Fatal($"Unhandled application error: {ex}");
            };

    }
}