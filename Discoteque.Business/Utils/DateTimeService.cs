using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discoteque.Business.Utilities
{
    public interface IDateTimeService
    {
        string ConvertToIsoDate(DateTime date);
        DateTime ConvertDateTimeToString (string date);
    }
    public class DateTimeService : IDateTimeService
    {
        public DateTime ConvertDateTimeToString(string date)
        {
            return DateTime.Parse(date); 
        }

        public string ConvertToIsoDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

    }
}
