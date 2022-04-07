using GitRepoHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper.UI.ViewModels
{
    public class WatchedDirViewModel : ViewModelBase
    {
        private readonly WatchedPath _context;

        public WatchedDirViewModel(WatchedPath dir)
        {
            _context = dir;
        }
    }
}
