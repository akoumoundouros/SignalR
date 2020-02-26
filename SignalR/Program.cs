using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Startup();
            RouteTable.Routes.MapConnection<MyEndPoint>("echo", "/echo");
            Console.ReadLine();
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.MapSignalR();
            }
        }

        public class MyEndPoint : PersistentConnection
        {
            protected override Task OnConnected(IRequest request, string connectionId)
            {
                return Connection.Broadcast("Connection " + connectionId + " connected");
            }
        }
    }
}
