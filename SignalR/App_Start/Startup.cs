using Microsoft.Owin;
using Owin;
using SignalR.Prysm.App_Start;
using SignalR.Prysm.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace SignalR.Prysm.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<PingConnection>("/ping-connection");
        }
    }
}