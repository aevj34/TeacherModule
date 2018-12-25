using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class UserAuthDto
    {
        public long id { get; set; }
        public String name { get; set; }
        public String bearerToken { get; set; }
        public bool isAuthenticated { get; set; }

        public int roleID { get; set; }
        public int schoolID { get; set; }

        public String shortName { get; set; }
        public String fullName { get; set; }

        public UserAuthDto()
        {
        }

    }
}
