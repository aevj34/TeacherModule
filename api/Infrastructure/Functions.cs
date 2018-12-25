
namespace api.Infrastructure
{
    public class Functions
    {

        public static string GetConnectionString()
        {
          return "Data Source=HP\\SQLEXPRESS;Initial Catalog=Logger;User ID=sa;Password=shaffer";
          // return "Data Source=academic.cgsarhyik5lf.sa-east-1.rds.amazonaws.com,1433;Initial Catalog=Logger;User ID=masterUserNameSa;Password=shaffermike753";
        }

    }
}
