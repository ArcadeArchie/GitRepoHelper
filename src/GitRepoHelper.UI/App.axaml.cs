using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GitRepoHelper.UI.ViewModels;
using GitRepoHelper.UI.Views;

namespace GitRepoHelper.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel() { Parent = desktop.MainWindow },
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
