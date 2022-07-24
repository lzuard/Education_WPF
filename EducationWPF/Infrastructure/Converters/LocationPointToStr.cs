using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EducationWPF.Infrastructure.Converters
{
    [ValueConversion(typeof(Point), typeof(string))]
    [MarkupExtensionReturnType(typeof(LocationPointToStr))]
    internal class LocationPointToStr : Converter
    {
        public override object Convert(object value, Type t, object p, CultureInfo c)
        {
            return value is Point point ? $"Lat: {point.X}; Lon: {point.Y}" : null;
        }

        public override object ConvertBack(object value, Type t, object p, CultureInfo c)
        {
            if(value is not string str) return null;

            var components = str.Split(';');
            var lat_str = components[0].Split(':')[1];
            var lon_str = components[1].Split(':')[1];

            var lat=double.Parse(lat_str);
            var lon=double.Parse(lon_str);

            return new Point(lat, lon);
        }
    }
}
