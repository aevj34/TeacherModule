
using System;

namespace api.Domain.Entity
  {
    public class Assistance
    {
	public Int32 assistanceID { get; set; }
	public String classTheme { get; set; }
	public String dateClass { get; set; }
	public String observation { get; set; }
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
        public Int32 evaluationPeriodID { get; set; }
        public Boolean active { get; set; }
    }
  }

 
