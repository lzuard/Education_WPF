using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationWPF.Models;

namespace EducationWPF.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
