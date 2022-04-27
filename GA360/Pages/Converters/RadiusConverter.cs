using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace GA360.Pages.Converters
{
    public class RadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            string retVal = null;
            if (val != null)
                switch (val)
                {
                    case 0:
                        retVal = "ER Location";
                        break;
                    case 1:
                        retVal = "0.25 mi";
                        break;
                    case 2:
                        retVal = "0.5 mi";
                        break;
                    case 3:
                        retVal = "1.0 mi";
                        break;
                    case 4:
                        retVal = "3.0 mi";
                        break;
                    case 5:
                        retVal = "5.0 mi";
                        break;
                    case 6:
                        retVal = "10.0 mi";
                        break;
                    default:
                        throw new InvalidDataException("value: must be <= 6");
                }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}