using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Prysm.Monitoring.WebApi.Diagnostics
{
    public class WebApiLogModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose()
        {
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            IEventSourceLogger logger = WebApiLogModule.GetLogger();
            logger.RequestBegin("Message from the WebApiLogModule.");
        }

        private void EndRequest(object sender, EventArgs e)
        {
            IEventSourceLogger logger = WebApiLogModule.GetLogger();
            logger.RequestEnd("Message from the WebApiLogModule.");
        }

        private static IEventSourceLogger GetLogger()
        {
            var container = GlobalConfiguration.Configuration.DependencyResolver;
            return (IEventSourceLogger)container.GetService(typeof(IEventSourceLogger));
        }
    }
}