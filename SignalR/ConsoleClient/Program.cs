using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static HubConnection connection { get; set; } 
        static IHubProxy chatHubProxy { get; set; }
        static void Main(string[] args)
        {
            connection = new HubConnection("http://localhost:55138/signalr");
            chatHubProxy = connection.CreateHubProxy("ChatHub");
            chatHubProxy.On("addNewMessageToPage", (value) => Console.WriteLine(value));
            RegisterClient("Group1");
            Console.WriteLine("Client Loaded\n");
            while (true)
            {
                Console.WriteLine("Press N to type a new message.");
                var input = Console.ReadLine();
                if (input.ToUpper().Contains("N"))
                {
                    input = Console.ReadLine();
                    SendMessage(input);
                }
            }
        }

        static async void RegisterClient(string group)
        {
            await connection.Start();
            await chatHubProxy.Invoke("RegisterClient", "Group1");
        }

        static async void SendMessage(string message)
        {
            await connection.Start();
            await chatHubProxy.Invoke("SendMessage", "Client", message);
        }
    }
}
