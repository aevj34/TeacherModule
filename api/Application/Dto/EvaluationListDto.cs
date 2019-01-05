
 using System;
  namespace api.Application.Dto
  {
    public class EvaluationListDto
    {
	public Int32 evaluationID { get; set; }
	public String name { get; set; }
    public decimal weight { get; set; }
    public Boolean isAverage { get; set; }
	public String evaluationType_abbreviation { get; set; }
    }
  }

 
