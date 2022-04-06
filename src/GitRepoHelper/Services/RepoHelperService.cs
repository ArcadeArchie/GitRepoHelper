using GitRepoHelper.Data;
using GitRepoHelper.Data.Abstractions;
using GitRepoHelper.Models;
using GitRepoHelper.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper.Services
{
    public interface IRepoHelperService : IRepository<AppDbContext>
    {
        ObservableCollection<WatchedPath> WatchedDirs { get; set; }

        bool AddWatchedDir(string? path);
    }

    public class RepoHelperService : IRepoHelperService
    {
        public AppDbContext Context { get; private set; }
        public ObservableCollection<WatchedPath> WatchedDirs { get; set; } = new ObservableCollection<WatchedPath>();


        public RepoHelperService(AppDbContext context)
        {
            Context = context;
        }


        public bool AddWatchedDir(string? path)
        {
            if (DirectoryHelper.DoesPathExist(path, out bool isFilePath) && !isFilePath)
            {
                WatchedDirs.Add(new WatchedPath { Path = path });
                return true;
            }

            return false;
        }

        public static int DirRepoCount(string path)
        {
            int count = 0;
            IEnumerable<string> subDirs = DirectoryHelper.GetChildren(path);
            foreach (string dir in subDirs)
            {
                count += DirectoryHelper.GetChildren(dir).Count(x => x.Contains(".git"));
            }
            return count;
        }
    }
}
