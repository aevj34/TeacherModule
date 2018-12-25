using System;

namespace api.Domain.Entity
  {
    public class Bucket
    {
	public Int32 bucketID { get; set; }
	public String name { get; set; }
	public Int32 amazonS3ID { get; set; }
	public Boolean currentBucket { get; set; }
	public Int32 schoolID { get; set; }
	public Boolean active { get; set; }
    }
  }

 
