
 using System;
  namespace api.Application.Dto
  {
    public class EvaluationDetailListDto
    {
	public Int32 evaluationDetailID { get; set; }
	public Int32 evaluationID { get; set; }
	public Int32 evaluationAverageID { get; set; }
        public decimal evaluation_weight { get; set; }
    }
  }

 
