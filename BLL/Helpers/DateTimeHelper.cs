using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToDateTime(string date)
        {
            DateTime result;
            DateTime.TryParseExact(date, "dd MMMM yyyy, HH:mm", CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out result);
            return result;
        }
    }
}
