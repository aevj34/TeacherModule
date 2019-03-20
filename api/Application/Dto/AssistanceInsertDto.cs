using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class AssistanceInsertDto
    {
        public Int32 schoolID { get; set; }
        public Int32 programmingID { get; set; }
        public List<AssistanceListDto> lessons { get; set; }
        public List<StudentAssistanceDto> students { get; set; }
    }
}
