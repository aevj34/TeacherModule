using api.Application.Dto;
using api.Domain.Entity;
using api.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common.Infrastructure.Security
{
  public class AmazonS3Factory
  {

    public static AmazonS3 setAmazonS3()
    {
      AmazonS3AdoNetRepository amazonS3AdoNetRepository = new AmazonS3AdoNetRepository();
      const int defaulAmazonS3ID = 1;
      return amazonS3AdoNetRepository.Obtain(defaulAmazonS3ID);
    }

  }
}
