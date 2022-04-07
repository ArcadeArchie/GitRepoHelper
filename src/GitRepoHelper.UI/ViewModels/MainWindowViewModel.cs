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
using System;
using System.Reactive.Linq;

namespace GitRepoHelper.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepoHelperService _repoHelperService;

        #region Properties
        public ObservableCollection<WatchedPath> WatchedPaths { get; set; } = new();

        [Reactive]
        public string? PathText { get; set; }
        public ReactiveCommand<string, Unit> AddPathCmd { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            _repoHelperService = App.Instance.GetRequiredService<IRepoHelperService>();
            AddPathCmd = ReactiveCommand.CreateFromTask<string>(HandleAddPath);
            LoadData().Subscribe(x => WatchedPaths.Add(x));
        }

        private IObservable<WatchedPath> LoadData() =>
            Observable.Create<WatchedPath>(async observer => 
            {
                foreach (var item in await _repoHelperService.LoadSavedDirs())
                {
                    observer.OnNext(item);
                }
            });

        private async Task HandleAddPath(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                if (App.Current == null || App.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                    return;
                var diag = new OpenFolderDialog();
                path = await diag.ShowAsync(desktop.MainWindow);
                if (string.IsNullOrEmpty(path))
                    return;
            }
            var item = await _repoHelperService.AddWatchedDirAsync(path);
            if (item == null)
                this.CreateWarningMsg("The given path leads to a file, add parent directory?", async btn =>
                {
                    item = await _repoHelperService.AddWatchedDirAsync(DirectoryHelper.GetParentDirFromFile(path));
                    if(item != null)
                        WatchedPaths.Add(item);
                    PathText = "";
                });
            else
            {
                WatchedPaths.Add(item);
                PathText = "";
            }
        }
    }
}
