using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationWPF.Services.Interfaces;

namespace EducationWPF.Services
{
    internal class WebServerService : IWebServerService
    {
        public bool Enabled { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
