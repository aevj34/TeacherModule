
 using System;
  namespace api.Application.Dto
  {
    public class AssistanceDetailDto
    {
	public Int32 assistanceDetailID { get; set; }
	public Int32 assistanceID { get; set; }
	public Int32 assistanceTypeID { get; set; }
	public Int32 studentID { get; set; }
	public Int32 schoolID { get; set; }
	public Int32 careerID { get; set; }
	public Int32 courseID { get; set; }
	public Int32 schoolYearID { get; set; }
	public Int32 classroomID { get; set; }
	public Int32 headquartersID { get; set; }
	public Int32 programmingID { get; set; }
	public Int32 teacherID { get; set; }
	public Int32 turnID { get; set; }
	public Int32 sectionID { get; set; }
	public Boolean active { get; set; }
    public String assistanceType_name { get; set; }
    public String assistanceType_abbreviation { get; set; }
        public Int32 evaluationPeriodID { get; set; }
        public Int32 enrollmentID { get; set; }
    }
  }

 
