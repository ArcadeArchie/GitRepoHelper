using System;
using System.Collections.Generic;
using System.Text;

namespace GitRepoHelper.Models
{
    public class WatchedPath
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? DisplayName { get; set; }
        public string Path { get; set; } = null!;
    }
}
