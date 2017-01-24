using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Prysm.Monitoring.WebApi;
using Prysm.Monitoring.WebApi.Diagnostics;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(ModuleConfig), "Start")]

namespace Prysm.Monitoring.WebApi
{
    public class ModuleConfig
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(WebApiLogModule));
        }
    }
}