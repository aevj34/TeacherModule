using api.Domain.Entity;
using api.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.Repository
{
    public class BucketAdoNetRepository: BucketRepository
    {


        public Bucket getCurrentBucket(Int32 amazonS3ID, Boolean currentBucket, Int32 schoolID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmamazonS3ID = null;
            SqlParameter prmcurrentBucket = null;
            SqlParameter prmschoolID = null;

            try
            {
                Bucket bucket;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetBucketByamazonS3IDBycurrentBucketByschoolID";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmamazonS3ID = new SqlParameter();
                prmamazonS3ID.ParameterName = "@amazonS3ID";
                prmamazonS3ID.SqlDbType = SqlDbType.Int;
                prmamazonS3ID.Value = amazonS3ID;
                command.Parameters.Add(prmamazonS3ID);

                prmcurrentBucket = new SqlParameter();
                prmcurrentBucket.ParameterName = "@currentBucket";
                prmcurrentBucket.SqlDbType = SqlDbType.Bit;
                prmcurrentBucket.Value = currentBucket;
                command.Parameters.Add(prmcurrentBucket);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                command.Connection.Open();
                reader = command.ExecuteReader();

                bucket = new Bucket();

                if (reader.Read())
                {
                    bucket.bucketID = reader.GetInt32(reader.GetOrdinal("bucketID"));
                    bucket.name = reader.GetString(reader.GetOrdinal("name"));
                    bucket.amazonS3ID = reader.GetInt32(reader.GetOrdinal("amazonS3ID"));
                    bucket.currentBucket = reader.GetBoolean(reader.GetOrdinal("currentBucket"));
                    bucket.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    bucket.active = reader.GetBoolean(reader.GetOrdinal("active"));
                }

                command.Connection.Close();
                conn.Dispose();

                return bucket;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
