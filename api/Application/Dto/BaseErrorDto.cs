using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class BaseErrorDto
    {
        public int Status { get; set; }
        public string Detail { get; set; }
        public int Code { get; set; }

        public BaseErrorDto(int status, string detail, int code)
        {
            Status = status;
            Detail = detail;
            Code = code;
        }
    }
}
