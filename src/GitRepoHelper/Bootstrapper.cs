using GitRepoHelper.Data;
using GitRepoHelper.Data.Abstractions;
using GitRepoHelper.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper
{
    public static class Bootstrapper
    {
        private static IServiceCollection? _appServices;

        public static IServiceCollection RegisterServices(IServiceCollection? platformServices = null)
        {
            var appServices = platformServices ?? new ServiceCollection();
            appServices.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite("Filename=app.db");
            });
            appServices.AddScoped<IRepoHelperService, RepoHelperService>();

            return appServices;
        }

        public static IServiceProvider Build()
        {
            if(_appServices == null)
                _appServices = RegisterServices();
            return _appServices.BuildServiceProvider();
        }
    }
}
