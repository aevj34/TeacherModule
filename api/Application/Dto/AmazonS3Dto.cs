
using api.Domain.Entity;
using api.Infrastructure.Repository;
using System;
  namespace api.Application.Dto
  {
    public class AmazonS3Dto
    {

	public Int32 amazonS3ID { get; set; }
	public String accessKeyId { get; set; }
	public String secretAccessKey { get; set; }
	public String region { get; set; }
	public String webAmazon { get; set; }
	public String prefix { get; set; }

    }
  }

 
