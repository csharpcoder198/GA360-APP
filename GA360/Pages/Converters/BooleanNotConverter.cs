using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace GA360.Pages.Converters
{
    public class BooleanNot : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            bool retVal = !val;
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}