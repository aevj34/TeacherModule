using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Repository
{
    public interface BucketRepository
    {
        Bucket getCurrentBucket(Int32 amazonS3ID, Boolean currentBucket, Int32 schoolID);
    }
}
