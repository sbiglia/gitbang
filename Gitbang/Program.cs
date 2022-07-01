using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.Logging;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Projektanker.Icons.Avalonia.MaterialDesign;
using Serilog;

namespace Gitbang
{
    static class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            try
            {
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

            using var log = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.File(Constants.LogFileName)
                .CreateLogger();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                app = app.UseManagedSystemDialogs();
            }

            return app;
        }

	}
}