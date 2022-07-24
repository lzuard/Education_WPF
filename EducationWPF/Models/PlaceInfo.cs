using System.Collections.Generic;
using System.Windows;

namespace EducationWPF.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }

        public virtual Point Location { get; set; }

        public virtual IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
