using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Prysm.Monitoring.WebApi.Logger;
using System.Diagnostics.Tracing;
using System.Web.Http;

namespace Prysm.Monitoring.WebApi
{
    public static class LoggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var listener = new ObservableEventListener();
            listener.EnableEvents("WebApiEventSource", EventLevel.LogAlways);
            listener.LogToFlatFile("Test.txt");
            listener.LogToServiceBus();
        }
    }
}