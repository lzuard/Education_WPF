using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWPF.WebLib;

namespace EWPF_Console
{
    internal class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequestReceived += OnRequestReceived;

            server.Start();
            Console.WriteLine("Server is on");

            Console.ReadLine();
        }

        private static void OnRequestReceived(object? sender, RequestReceiverEventArgs e)
        {
            var context=e.Context;

            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer=new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("Hello from test web server!");
        }
    }
}
