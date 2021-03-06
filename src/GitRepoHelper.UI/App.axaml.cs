using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GitRepoHelper.UI.ViewModels;
using GitRepoHelper.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GitRepoHelper.UI
{
    public partial class App : Application
    {
        public IServiceProvider AppServices { get; private set; }

        public static App Instance
        {
            get
            {
                if (Current is not App app || Current is null)
                    throw new InvalidOperationException("Invalid App Type");
                return app;
            }
        }

        public App()
        {
            AppServices = Bootstrapper.Build();
        }

        #region Application
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
        #endregion
    }
    public static class AppServicesExtentions
    {
        public static TService GetRequiredService<TService>(this App app) where TService : notnull
        {
            return app.AppServices.GetRequiredService<TService>();
        }
    }
}
