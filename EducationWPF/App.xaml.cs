using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using EducationWPF.Services;
using EducationWPF.Services.Interfaces;
using EducationWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EducationWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost _host;

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath()) : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string path = null) => path;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);

            host.Dispose();
            _host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => 
            services
            .RegisterServices()
            .RegisterViewModels();

    }
}
