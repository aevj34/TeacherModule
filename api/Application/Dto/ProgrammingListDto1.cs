using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class ProgrammingListDto1
    {
        public Int32 programmingID { get; set; }
        public String career_name { get; set; }
        public String course_name { get; set; }
        public Int32 grade { get; set; }
        public int schoolID { get; set; }
        public String evaluationPeriod_name { get; set; }
        public String section_name { get; set; }
        public String turn_name { get; set; }
        public List<AssistanceListDto> lessons { get; set; }
    }
}
