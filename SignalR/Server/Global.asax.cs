using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Server.Startup))]
namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

    }
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Configuration(new Microsoft.Owin.Builder.AppBuilder());
            GlobalHost.ConnectionManager.GetConnectionContext<MyEndPoint>().Connection.Broadcast("Server Started.");
           
        }

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            var hubConfig = new HubConfiguration();
            hubConfig.EnableDetailedErrors = true;
            hubConfig.EnableJavaScriptProxies = true;
            app.MapSignalR("/signalr", hubConfig);
        }
    }
}
