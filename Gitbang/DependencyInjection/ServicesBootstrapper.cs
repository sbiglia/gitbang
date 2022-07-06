using Gitbang.Core.Services;
using Splat;

namespace Gitbang.DependencyInjection;

public class ServicesBootstrapper
{

    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {

        services.RegisterLazySingleton<IEnvironmentService>(()=> new EnvironmentService());

    }


}