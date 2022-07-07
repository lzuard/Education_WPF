using System.Collections.Generic;

namespace EducationWPF.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> ProvinceCounts { get;set;}
    }
}
