using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhweatherapp.Extensions
{
    public static class DateExtensions
    {
        public static bool IsNextDay(this DateTime date, DateTime? prev)
        {
            var check = prev.Value + TimeSpan.FromDays(1);
            return check == date;
        }
    }
}
