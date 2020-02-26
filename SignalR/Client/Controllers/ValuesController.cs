using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async void Get()
        {
            HubConnection connection = new HubConnection("http://localhost:55138/signalr");
            IHubProxy chatHubProxy = connection.CreateHubProxy("ChatHub");
            chatHubProxy.On("addNewMessageToPage", (value) => System.Threading.SynchronizationContext.Current
                .Post(delegate
                    {
                        App.Log += value;
                    }, 
                    null
                ));
            await connection.Start();
            await chatHubProxy.Invoke("SendMessage", "Client", "Hello I'm .net client");
        }

        // GET api/values/5
        public string Get(bool id)
        {
            
            return App.Log;
        }

        //[System.Web.Http.Route("/api/vales/addNewMessageToPage")]
        //public void addNewMessageToPage(string UserName, string Message)
        //{

        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
            var s = value;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
