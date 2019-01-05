using api.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Entity
{
    public class AmazonS3
    {

        public Int32 amazonS3ID { get; set; }
        public String accessKeyId { get; set; }
        public String secretAccessKey { get; set; }
        public String region { get; set; }
        public String webAmazon { get; set; }
        public String prefix { get; set; }


        public Bucket getCurrentBucket(int schoolID)
        {
            BucketAdoNetRepository bucketAdoNetRepository = new BucketAdoNetRepository();
            const bool currentBucket = true;

            Bucket bucket = new Bucket();
            bucket = bucketAdoNetRepository.getCurrentBucket(this.amazonS3ID, currentBucket, schoolID);

            return bucket;
        }

        public Folder getTeacherFolder(int schoolID, int bucketID)
        {
            FolderAdoNetRepository folderAdoNetRepository = new FolderAdoNetRepository();
            const bool active = true;
            const int folderTeachers = 2;

            Folder folder = new Folder();
            folder = folderAdoNetRepository.GetByfolderTypeIDBybucketIDByschoolIDByactive(folderTeachers, bucketID, schoolID, active);

            return folder;
        }

        public Folder getStudentsFolder(int schoolID, int bucketID)
        {
            FolderAdoNetRepository folderAdoNetRepository = new FolderAdoNetRepository();
            const bool active = true;
            const int folderStudents = 1;

            Folder folder = new Folder();
            folder = folderAdoNetRepository.GetByfolderTypeIDBybucketIDByschoolIDByactive(folderStudents, bucketID, schoolID, active);

            return folder;
        }

        public String getTeacherEndPoint(int schoolID, String ImageKey)
        {
            Bucket bucket = new Bucket();
            bucket = getCurrentBucket(schoolID);

            string bucketName = "";
            string folderTeachers = "";

            if (bucket != null)
            {
                bucketName = bucket.name;
                Folder folder = new Folder();
                folder = getTeacherFolder(schoolID, bucket.bucketID);
             
                if (folder != null)
                {
                    folderTeachers = folder.name;
                }               
            }

            return this.prefix + this.region + "." + this.webAmazon + "/" + bucketName  + "/" + folderTeachers + "/" + ImageKey;
        }

        public String getStudentEndPoint(int schoolID, String ImageKey)
        {
            Bucket bucket = new Bucket();
            bucket = getCurrentBucket(schoolID);

            string bucketName = "";
            string folderStudents = "";

            if (bucket != null)
            {
                bucketName = bucket.name;
                Folder folder = new Folder();
                folder = getStudentsFolder(schoolID, bucket.bucketID);

                if (folder != null)
                {
                    folderStudents = folder.name;
                }
            }

            return this.prefix + this.region + "." + this.webAmazon + "/" + bucketName + "/" + folderStudents + "/" + ImageKey;
        }


    }
}
