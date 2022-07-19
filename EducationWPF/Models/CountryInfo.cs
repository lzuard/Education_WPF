using System.Collections.Generic;

namespace EducationWPF.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get;set;}
    }
}
