using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationWPF.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EducationWPF.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();
            services.AddTransient<IAsyncDataService, AsyncDataService>();
            services.AddTransient<IWebServerService ,WebServerService>();

            return services;
        }
    }
}
