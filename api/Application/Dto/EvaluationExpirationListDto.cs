
 using System;
  namespace api.Application.Dto
  {
    public class EvaluationExpirationListDto
    {
	public Int32 evaluationExpirationID { get; set; }
	public DateTime startDate { get; set; }
	public DateTime endDate { get; set; }
	public Int32 programmingID { get; set; }
	public Int32 evaluationID { get; set; }
	public bool active { get; set; }

        public String startDateAgo { get; set; }
        public String endDateAgo { get; set; }
        public String startDateShow { get; set; }
        public String endDateShow { get; set; }

    }
  }

 
