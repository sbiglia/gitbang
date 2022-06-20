using Gitbang.Core.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gitbang.Core
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;

        private ServiceProvider _container;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
        }

        ~ServiceLocator()
        {
            Dispose();
        }

        #region properties

        public static ServiceLocator Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ServiceLocator();
                }

                return _instance;
            }
        }

        internal ServiceProvider Container
        {
            get
            {
                if (_container == null)
                {
                    _container = RegisterServices();
                }

                return _container;
            }

        }

        internal bool IsCacheInitialized { get; private set; }

        #endregion

        ServiceProvider RegisterServices()
        {
            var container = new ServiceCollection();

            // add shared services in the beginning
            //container.AddSingleton<ICommandService, CommandService>();
            //container.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            //container.AddSingleton<IConfigurationService, ConfigurationService>();
            //container.AddSingleton<IPlatformService, PlatformService>();

            var assemblies = new List<Assembly>();
            var currentDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in currentDomainAssemblies)
            {
                if (assembly.FullName.StartsWith("Gitbang.", StringComparison.InvariantCultureIgnoreCase) &&
                    !assembly.FullName.StartsWith("Gitbang.Core", StringComparison.InvariantCultureIgnoreCase))
                    assemblies.Add(assembly);
            }

            foreach (var assembly in assemblies)
                AddToContainer(container, assembly);

            return container.BuildServiceProvider();
        }

        void UnregisterServices()
        {
            Container?.Dispose();
        }

        public void Dispose()
        {
            if (IsCacheInitialized)
            {
                //_serviceCache.Clear();
                //_providersCache.Clear();

                IsCacheInitialized = false;
            }

            UnregisterServices();
        }

        void AddToContainer(ServiceCollection collection, Assembly assembly)
        {
            var viewModelBaseType = typeof(ViewModelBase);

            foreach (var type in assembly.GetExportedTypes())
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                if (viewModelBaseType.IsAssignableFrom(type))
                    collection.AddTransient(type);
            }
        }

    }
}
