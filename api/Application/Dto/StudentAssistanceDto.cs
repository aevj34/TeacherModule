using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class StudentAssistanceDto
    {
        public Int32 orderNumber { get; set; }
        public Int32 studentID { get; set; }
        public String student_code { get; set; }
        public String student_name { get; set; }
        public String student_firstName { get; set; }
        public String student_lastName { get; set; }
        public String EndPoint { get; set; }
        public List<AssistancexStudentDto> assistances { get; set; }
        public List<AssistanceTypeListDto> assistanceTypes { get; set; }
    }
}
