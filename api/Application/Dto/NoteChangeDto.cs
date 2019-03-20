
 using System;
  namespace api.Application.Dto
  {
    public class NoteChangeDto
    {
	public Int32 noteChangeID { get; set; }
	public Int32 noteID { get; set; }
	public string note { get; set; }
	public Int32 teacherID { get; set; }
	public Int32 userID { get; set; }
	public DateTime changeDate { get; set; }
	public Int32 schoolID { get; set; }
    }
  }

 
