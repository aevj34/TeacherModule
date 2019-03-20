using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
    public static class TimerAgo
    {

    public static string TimeAgo(this DateTime dateTime)
    {
      string result = string.Empty;

      if (dateTime.Year > 1900)
      {
      
        var timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan <= TimeSpan.FromSeconds(60))
        {
          result = string.Format("{0} seg", timeSpan.Seconds);
        }
        else if (timeSpan <= TimeSpan.FromMinutes(60))
        {
          result = timeSpan.Minutes >= 2 ?
              String.Format("{0} min", timeSpan.Minutes) :
              "1 min";
        }
        else if (timeSpan <= TimeSpan.FromHours(24))
        {
          result = timeSpan.Hours >= 2 ?
              String.Format("{0} hrs", timeSpan.Hours) :
              "1 hr";
        }
        else if (timeSpan <= TimeSpan.FromDays(30))
        {
          result = timeSpan.Days >= 2 ?
              String.Format("{0} días", timeSpan.Days) :
              "ayer";
        }
        else if (timeSpan <= TimeSpan.FromDays(365))
        {
          result = timeSpan.Days >= 60 ?
              String.Format("{0} meses", timeSpan.Days / 30) :
              "1 mes";
        }
        else
        {
          result = timeSpan.Days >= 730 ?
              String.Format("{0} años", timeSpan.Days / 365) :
              "1 año";
        }
      }
      else
      {
        result = "";
      }


      return result;
    }

        public static string TimeAfter(this DateTime dateTime)
        {
            string result = string.Empty; 

            if (dateTime.Year > 1900)
            {
                var timeSpan = dateTime.Subtract(DateTime.Now);

                if (timeSpan <= TimeSpan.FromSeconds(60))
                {
                    result = string.Format("{0} seg", timeSpan.Seconds);
                }
                else if (timeSpan <= TimeSpan.FromMinutes(60))
                {
                    result = timeSpan.Minutes >= 2 ?
                        String.Format("{0} min", timeSpan.Minutes) :
                        "1 min";
                }
                else if (timeSpan <= TimeSpan.FromHours(24))
                {
                    result = timeSpan.Hours >= 2 ?
                        String.Format("{0} hrs", timeSpan.Hours) :
                        "1 hr";
                }
                else if (timeSpan <= TimeSpan.FromDays(30))
                {
                    result = timeSpan.Days >= 2 ?
                        String.Format("{0} días", timeSpan.Days) :
                        "1 día";
                }
                else if (timeSpan <= TimeSpan.FromDays(365))
                {
                    result = timeSpan.Days >= 60 ?
                        String.Format("{0} meses", timeSpan.Days / 30) :
                        "1 mes";
                }
                else
                {
                    result = timeSpan.Days >= 730 ?
                        String.Format("{0} años", timeSpan.Days / 365) :
                        "1 año";
                }
            }
            else
            {
                result = "";
            }


            return result;
        }


        public static string TimeShow(this DateTime dateTime, string format)
    {
      string result = string.Empty;

      if (dateTime.Year == 1900)
      {
        result = "";
      }
      else
      {
        result = dateTime.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
      }
      return result;
    }


    public static bool IsDate(this string text)
    {
      if (!string.IsNullOrEmpty(text))
      {
        DateTime result = DateTime.MinValue;
        foreach (CultureInfo cultureInfo in
                 CultureInfo.GetCultures(CultureTypes.AllCultures))
        {

            if (DateTime.TryParse(text, cultureInfo,
                                  DateTimeStyles.None,
                                  out result))
              return true;
        }
      }

      return false;
    }


  }
}
