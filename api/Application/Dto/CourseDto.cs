using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class CourseDto
    {
        public int courseID { get; set; }
        public Int32 credits { get; set; }
        public Int32 evaluationFormulaID { get; set; }
        public Int32 grade { get; set; }
        public Int32 programmingID { get; set; }
        public String course_name { get; set; }
        public Int32 course_grade { get; set; }
        public String section_name { get; set; }
        public String turn_name { get; set; }
        public List<TutorNoteDto> notes { get; set; }
        public Int32 orderNumber { get; set; }
        public List<EvaluationListDto> evaluations { get; set; }
        public String Concatevaluations { get; set; }
        public List<AssistancexStudentDto> completeAssistances { get; set; }
        public List<AssistanceListDto> lessons { get; set; }
    }
}
