using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationWPF.Services.Interfaces;
using EWPF.WebLib;

namespace EducationWPF.Services
{
    internal class WebServerService : IWebServerService
    {
        private WebServer _server = new WebServer(8080);

        public bool Enabled { get=>_server.Enabled; set=>_server.Enabled=value; }

        public void Start() => _server.Start();

        public void Stop() => _server.Stop();


        public WebServerService() => _server.RequestReceived += OnRequestReceived;

        private void OnRequestReceived(object? sender, RequestReceiverEventArgs e)
        {
            Thread.Sleep(2000); //to make it look like server is processing something
            using var writer = new StreamWriter(e.Context.Response.OutputStream);
            writer.WriteLine("Application EWPF");
        }
    }
}
