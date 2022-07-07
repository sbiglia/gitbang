using Gitbang.Core.Services;
using Gitbang.Core.Settings.Interfaces;
using Gitbang.Managers;
using Gitbang.Managers.Interfaces;
using Splat;

namespace Gitbang.DependencyInjection;

public class ServicesBootstrapper
{

    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton<IEnvironmentService>(()=> new EnvironmentService());
        services.RegisterLazySingleton<IReposIndexManager>(() => new ReposIndexManager());
    }


}