using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class AssistancexStudentDto
    {
        public Int32 assistanceDetailID { get; set; }
        public Int32 assistanceID { get; set; }
        public Int32 assistanceTypeID { get; set; }
        public String assistanceType_abbreviation { get; set; }
    }
}
