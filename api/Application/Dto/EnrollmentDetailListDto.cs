
 using System;
using System.Collections.Generic;

namespace api.Application.Dto
  {
    public class EnrollmentDetailListDto
    {
      public Int32 orderNumber { get; set; }
      public Int32 enrollmentDetailID { get; set; }
      public Int32 enrollmentID { get; set; }
      public Int32 schoolID { get; set; }
      public Int32 programmingID { get; set; }
      public Int32 studentID { get; set; }
      public String student_code { get; set; }
      public String student_name { get; set; }
      public String student_firstName { get; set; }
      public String student_lastName { get; set; }
      public Boolean active { get; set; }
      public String EndPoint { get; set; }
      public String ImageKey { get; set; }
      public Int32 evaluations { get; set; }
      public List<NoteListDto> notes { get; set; }
      public List<AssistanceDetailDto> assistances { get; set; }
      public List<AssistanceTypeListDto> assistanceTypes { get; set; }

    }
  }

 
