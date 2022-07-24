using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace EducationWPF.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(Add))]
    internal class Add : Converter
    {
        [ConstructorArgument("K")]
        public double B { get; set; } = 1;

        public Add()
        {

        }

        public Add(double b) => this.B = b;

        public override object Convert(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, c);
            return x + B;

        }

        public override object ConvertBack(object value, Type t, object p, CultureInfo c)
        {
            if (value is null) return null; var x = System.Convert.ToDouble(value, c);
            return x - B;
        }
    }
}