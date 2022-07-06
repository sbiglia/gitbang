using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;

namespace Gitbang.DependencyInjection
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {

            ServicesBootstrapper.Register(services, resolver);
            ViewModelBootstrapper.Register(services, resolver);

        }
    }
}
