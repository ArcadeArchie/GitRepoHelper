using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GitRepoHelper.Util;
using GitRepoHelper.Models;
using GitRepoHelper.UI.Util;
using GitRepoHelper.UI.Views;
using GitRepoHelper.Services;
using GitRepoHelper.Data.Abstractions;
using GitRepoHelper.Data;

namespace GitRepoHelper.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepoHelperService _repoHelperService;

        #region Properties
        public ObservableCollection<WatchedPath> WatchedPaths => _repoHelperService.WatchedDirs;

        [Reactive]
        public string? PathText { get; set; }
        public ReactiveCommand<string, Unit> AddPathCmd { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            _repoHelperService = App.Instance.GetRequiredService<IRepoHelperService>();
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

            if(!_repoHelperService.AddWatchedDir(path))
                this.CreateWarningMsg("The given path leads to a file, add parent directory?", btn =>
                {
                    WatchedPaths.Add(new WatchedPath { Path = DirectoryHelper.GetParentDirFromFile(path) });
                    PathText = "";
                });
            else
                PathText = "";
        }
    }
}
