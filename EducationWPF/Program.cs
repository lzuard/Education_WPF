using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EducationWPF
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(App.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) => cfg
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true))
                .ConfigureServices(App.ConfigureServices);
    }
}
