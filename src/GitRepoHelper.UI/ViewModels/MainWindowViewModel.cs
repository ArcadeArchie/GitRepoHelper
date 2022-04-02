using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GitRepoHelper.Util;
using GitRepoHelper.Models;

namespace GitRepoHelper.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<WatchedPath> WatchedPaths { get; set; } = new();

        [Reactive]
        public string? PathText { get; set; }
        public ReactiveCommand<string, Unit> AddPathCmd { get; set; }

        public MainWindowViewModel()
        {
            AddPathCmd = ReactiveCommand.CreateFromTask<string>(HandleAddPath);
        }

        private async Task HandleAddPath(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                if (App.Current == null || App.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                    return;
                var diag = new OpenFolderDialog();
                path = await diag.ShowAsync(desktop.MainWindow);
            }
            if (DirectoryHelper.DoesPathExist(path, out bool isFilePath))
            {
                if (isFilePath)
                {
                    //if (App.Current == null || App.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                    //    return;
                    CreateWarningMsg("The give path leads to a file");
                }
                else
                {
                    WatchedPaths.Add(new WatchedPath { Path = path });
                }
            }
        }
    }
}