using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public class TimeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;
            if (value is DateTime time && targetType.IsAssignableTo(typeof(string)) == true && par == "time")
            {
                string str_time = time.ToString("HH:mm");
                return str_time;
            }
            if (value is DateTime date_time && targetType.IsAssignableTo(typeof(string)) == true && par == "date_time")
            {
                string str_time = date_time.ToString("HH:mm") + ", " + date_time.ToString("dd.MM");
                return str_time;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
