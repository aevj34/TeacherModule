using api.Application.Dto;
using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.Repository
{
    public class AmazonS3AdoNetRepository
    {


        public AmazonS3 Obtain(Int32 amazonS3ID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmamazonS3ID = null;

            try
            {
                AmazonS3 amazonS3;
                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "ObtainAmazonS3";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmamazonS3ID = new SqlParameter();
                prmamazonS3ID.ParameterName = "@amazonS3ID";
                prmamazonS3ID.SqlDbType = SqlDbType.Int;
                prmamazonS3ID.Value = amazonS3ID;
                command.Parameters.Add(prmamazonS3ID);

                command.Connection.Open();
                reader = command.ExecuteReader();


                amazonS3 = new AmazonS3();
                if (reader.Read())
                {
                    amazonS3.amazonS3ID = reader.GetInt32(reader.GetOrdinal("amazonS3ID"));
                    amazonS3.accessKeyId = reader.GetString(reader.GetOrdinal("accessKeyId"));
                    amazonS3.secretAccessKey = reader.GetString(reader.GetOrdinal("secretAccessKey"));
                    amazonS3.region = reader.GetString(reader.GetOrdinal("region"));
                    amazonS3.webAmazon = reader.GetString(reader.GetOrdinal("webAmazon"));
                    amazonS3.prefix = reader.GetString(reader.GetOrdinal("prefix"));
                }

                command.Connection.Close();
                conn.Dispose();

                return amazonS3;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
