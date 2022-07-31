using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EducationWPF.Infrastructure.Converters
{
    internal class ParametricMultiplyValueConverter : Freezable, IValueConverter
    {
        #region Value Property : double - Add value


        /// <summary>
        /// Add value
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(ParametricMultiplyValueConverter),
                new PropertyMetadata(1.0));

        /// <summary>
        /// Add value
        /// </summary>
        [Description("Add value")]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion

        public object Convert(object v, Type tt, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v,c);
            return value * Value;
        }

        public object ConvertBack(object v, Type tt, object p, CultureInfo c)
        {
            var value = System.Convert.ToDouble(v, c);
            return value / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultiplyValueConverter{Value = Value};
    }
}
