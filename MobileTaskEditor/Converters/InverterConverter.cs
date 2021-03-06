using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileTaskEditor.Converters
{
    public class InverterConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert(value);
        }

        private static object Invert(object value)
        {
            switch (value)
            {
                case bool b: return !b;
                case int i: return -i;
                default: throw new ArgumentOutOfRangeException(nameof(value), value, "Cannot invert unknown type.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert(value);
        }

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}