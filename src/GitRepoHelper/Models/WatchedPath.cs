using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GitRepoHelper.Models
{
    public class WatchedPath
    {
        [Key]
        [NotNull]
        public Guid? Id { get; set; }
        public string? DisplayName { get; set; }
        public string Path { get; set; } = null!;
    }
}
