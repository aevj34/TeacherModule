using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class ExpiredDto
    {
            public bool isExpired { get; set; }
            public String expiredMessage { get; set; }
            public String expiredAfter { get; set; }
            public bool isAboutExpired { get; set; }
    }
}
