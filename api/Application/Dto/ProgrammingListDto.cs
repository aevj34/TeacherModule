
using api.Domain.Entity;
using System;
using System.Collections.Generic;

namespace api.Application.Dto
  {
    public class ProgrammingListDto
    {
	public Int32 programmingID { get; set; }
	public String career_name { get; set; }
	public String classroom_name { get; set; }
	public String course_name { get; set; }
	public Int32 course_credits { get; set; }
	public Int32 grade { get; set; }
	public String evaluationPeriod_name { get; set; }
	public String section_name { get; set; }
	public String turn_name { get; set; }
    public Int32 evaluationFormulaID { get; set; }
    public int schoolID { get; set; }
    public Int32 courseID { get; set; }
    public Int32 schoolYearID { get; set; }
    public Int32 careerID { get; set; }
    public Int32 sectionID { get; set; }
    public Int32 turnID { get; set; }
    public Int32 periodTypeID { get; set; }
     public Int32 evaluationPeriodID { get; set; }
     public Int32 headquartersID { get; set; }
        public Int32 teacherID { get; set; }
        public List<EnrollmentDetailListDto> students { get; set; }
        public int teacherTypeID { get; set; }
        public int assistanceID { get; set; }
        public int assistanceTypeID { get; set; }
        public int classroomID { get; set; }
        public List<Assistance> headerAssistances { get; set; }

        public String endPoint { get; set; }
        public String imageKey { get; set; }

        public String teacher_name { get; set; }
        public String teacher_firstName { get; set; }
        public String teacher_lastName { get; set; }

    }
  }

 
