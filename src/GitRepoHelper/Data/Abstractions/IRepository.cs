using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper.Data.Abstractions
{
    public interface IRepository<T> where T : DbContext
    {
        T Context { get; }
    }
}
