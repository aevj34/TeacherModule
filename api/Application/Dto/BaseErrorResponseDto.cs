using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class BaseErrorResponseDto
    {
        public List<BaseErrorDto> Errors { get; set; } = new List<BaseErrorDto>();
    }
}
