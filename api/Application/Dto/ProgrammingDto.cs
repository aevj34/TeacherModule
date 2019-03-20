
using System;
namespace api.Application.Dto
{
    public class ProgrammingDto
    {
        public Int32 programmingID { get; set; }
        public Int32 teacherID { get; set; }
        public Int32 classroomID { get; set; }
        public Int32 courseID { get; set; }
        public Int32 schoolYearID { get; set; }
        public Int32 schoolID { get; set; }
        public Int32 careerID { get; set; }
        public Int32 sectionID { get; set; }
        public Int32 turnID { get; set; }
        public Int32 evaluationFormulaID { get; set; }
        public Int32 vacant { get; set; }
        public Int32 periodTypeID { get; set; }
        public Int32 evaluationPeriodID { get; set; }
        public Int32 headquartersID { get; set; }
        public Int32 grade { get; set; }
        public Boolean active { get; set; }
        public String career_name { get; set; }
        public String course_name { get; set; }
        public String school_name { get; set; }
        public String section_name { get; set; }
        public String teacher_name { get; set; }
        public String teacher_firstName { get; set; }
        public String teacher_lastName { get; set; }
        public String turn_name { get; set; }
        public int classesNumber { get; set; }

    }
}


