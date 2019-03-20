
 using System;
  namespace api.Application.Dto
  {
    public class NoteListDto
    {
	 public string note { get; set; }
        public string lastNote { get; set; }
        public Int32 noteID { get; set; }
     public Int32 studentID { get; set; }
	 public Int32 evaluationID { get; set; }
     public Int32 enrollmentID { get; set; }
     public Int32 enrollmentDetailID { get; set; }
     public Int32 courseID { get; set; }
        public Int32 schoolID { get; set; }
        public Int32 headquartersID { get; set; }

        public Int32 careerID { get; set; }
        public Int32 schoolYearID { get; set; }
        public Int32 programmingID { get; set; }

        public Int32 sectionID { get; set; }
        public Int32 turnID { get; set; }
        public Int32 evaluationFormulaID { get; set; }
        public Int32 evaluationPeriodID { get; set; }
        public Int32 periodTypeID { get; set; }
        public Int32 grade { get; set; }
        public Boolean active { get; set; }
        public Boolean isChanged { get; set; }
        public Boolean isAverage { get; set; }
        public Boolean isApproved { get; set; }
        public Boolean isExpired { get; set; }

        public Boolean isInputText { get; set; }
    }
}

 
