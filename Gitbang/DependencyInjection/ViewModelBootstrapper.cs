using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Managers.Interfaces;
using Gitbang.ViewModels;
using Splat;

namespace Gitbang.DependencyInjection
{
    public class ViewModelBootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<MainViewViewModel>( ()=> new MainViewViewModel(resolver.GetService<IReposIndexManager>()));

        }

    }
}
