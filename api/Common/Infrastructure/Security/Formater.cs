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


    }
}
