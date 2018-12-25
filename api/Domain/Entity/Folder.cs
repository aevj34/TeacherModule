using System;

namespace api.Domain.Entity
  {
    public class Folder
    {
	public Int32 folderID { get; set; }
	public String name { get; set; }
	public Int32 folderTypeID { get; set; }
	public String noImage { get; set; }
	public Int32 bucketID { get; set; }
	public Int32 schoolID { get; set; }
	public Boolean active { get; set; }
    }
  }

 
