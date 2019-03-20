
 using System;
  namespace api.Application.Dto
  {
    public class AssistanceTypeListDto
    {
	public Int32 assistanceTypeID { get; set; }
	public String name { get; set; }
	public String abbreviation { get; set; }
    public bool isSelected { get; set; }
    public bool isLack { get; set; }
    }
  }

 
