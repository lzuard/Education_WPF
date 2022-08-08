using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationWPF.Services.Interfaces
{
    internal interface IWebServerService
    {
        bool Enabled { get; set; }

        void Start();

        void Stop();
    }
}
