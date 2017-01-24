using Microsoft.Practices.Unity;
using Prysm.Monitoring.WebApi.Diagnostics;
using System.Web.Http;
using Unity.WebApi;

namespace Prysm.Monitoring
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IEventSourceLogger, WebApiEventSource>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}