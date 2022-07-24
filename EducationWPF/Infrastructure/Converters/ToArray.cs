using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EducationWPF.Infrastructure.Converters
{
    internal class ToArray : MultiValueConverter
    {
        public override object Convert(object[] vv, Type t, object p, CultureInfo c)
        {
            var collection = new CompositeCollection();
            foreach (var value in vv)
                collection.Add(value);
            return collection;
        }

        //public override object[] ConvertBack(object v, Type[] tt, object p, CultureInfo c) => v as object[];
    }
}
