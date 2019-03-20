using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
    public static  class Formater
    {

    public static string ShortDateTime()
    {
      return "dd/MM/yyyy";
    }

    public static string SortableDateTime()
    {
      return "yyyy-MM-dd";
    }

        public static string getShortName(string strMonth)
        {
            string strShortName = "";

            if (strMonth == "1")
                strShortName = "ene";
            else if (strMonth == "2")
                strShortName = "feb";
            else if (strMonth == "3")
                strShortName = "mar";
            else if (strMonth == "4")
                strShortName = "abr";
            else if (strMonth == "5")
                strShortName = "may";
            else if (strMonth == "6")
                strShortName = "jun";
            else if (strMonth == "7")
                strShortName = "jul";
            else if (strMonth == "8")
                strShortName = "ago";
            else if (strMonth == "9")
                strShortName = "set";
            else if (strMonth == "10")
                strShortName = "oct";
            else if (strMonth == "11")
                strShortName = "nov";
            else if (strMonth == "12")
                strShortName = "dic";

            return strShortName;
        }

    }
}
