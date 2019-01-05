
using System;

namespace api.Domain.Entity
  {
    public class Note
    {
	public Int32 noteID { get; set; }
	public Int32 enrollmentID { get; set; }
	public Int32 enrollmentDetail { get; set; }
	public Decimal note { get; set; }
	public Int32 studentID { get; set; }
	public Int32 courseID { get; set; }
	public Int32 schoolID { get; set; }
	public Int32 headquartersID { get; set; }
	public Int32 careerID { get; set; }
	public Int32 schoolYearID { get; set; }
	public Int32 programmingID { get; set; }
	public Int32 evaluationPeriodID { get; set; }
	public Int32 turnID { get; set; }
	public Int32 sectionID { get; set; }
	public Int32 periodTypeID { get; set; }
	public Int32 evaluationID { get; set; }
	public Boolean active { get; set; }
    }
  }

 
