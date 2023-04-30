using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public class StatusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;
            DateTime now_date = DateTime.Now;
            if (value is DepartureTableItem item && targetType.IsAssignableTo(typeof(string)) == true && par == "departure")
            {
                string str;
                if (now_date.CompareTo(item.EstimatedTime) > 0)
                {
                    str = "Вылетел";
                    return str;
                }
                else
                {
                    if (now_date.CompareTo(item.RegistrationStartTime) > 0 && now_date.CompareTo(item.BoardingTime) < 0)
                    {
                        str = "Регистрация";
                        return str;
                    }
                    if (now_date.CompareTo(item.BoardingTime) > 0 && now_date.CompareTo(item.EstimatedTime) < 0)
                    {
                        str = "Идёт посадка";
                        return str;
                    }
                    if (now_date.CompareTo(item.RegistrationStartTime) < 0)
                    {
                        str = "По расписанию";
                        return str;
                    }
                }
            }
            if (value is DateTime arr_time && targetType.IsAssignableTo(typeof(string)) == true && par == "arrival")
            {
                string str;
                if (now_date.CompareTo(arr_time) > 0)
                {
                    str = "Рейс прибыл";
                    return str;//Ожидается
                }
                else
                {
                    if (now_date.CompareTo(arr_time) < 0)
                    {
                        str = "По расписанию";
                        return str;
                    }
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
