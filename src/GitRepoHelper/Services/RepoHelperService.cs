using GitRepoHelper.Data;
using GitRepoHelper.Data.Abstractions;
using GitRepoHelper.Models;
using GitRepoHelper.Util;
using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<WatchedPath>> LoadSavedDirs();

        Task<WatchedPath?> AddWatchedDirAsync(string? path);
    }

    public class RepoHelperService : IRepoHelperService
    {
        public AppDbContext Context { get; private set; }


        public RepoHelperService(AppDbContext context)
        {
            Context = context;
        }


        public async Task<IEnumerable<WatchedPath>> LoadSavedDirs() 
            => await Context.WatchedPaths.ToListAsync();

        public async Task<WatchedPath?> AddWatchedDirAsync(string? path)
        {
            if (DirectoryHelper.DoesPathExist(path, out bool isFilePath) && !isFilePath)
            {
                if (string.IsNullOrEmpty(path))
                    return null;
                var item = new WatchedPath { Id = Guid.NewGuid(), Path = path };
                if (!await Context.WatchedPaths.AnyAsync(x => x.Path == item.Path))
                {
                    await Context.WatchedPaths.AddAsync(item);
                    await Context.SaveChangesAsync();
                }
                return item;
            }

            return null;
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
