
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
using api.Domain.Entity;
using api.Common.Infrastructure.Security;

namespace api.Infrastructure.Repository
 {

 public class EnrollmentDetailAdoNet:EnrollmentDetailRepository
 {

         public  List<EnrollmentDetailListDto> GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmprogrammingID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                EnrollmentDetailListDto enrollmentDetail;
                List<EnrollmentDetailListDto> lstEnrollmentDetails;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetEnrollmentDetailByprogrammingIDByschoolID";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = programmingID;
                command.Parameters.Add(prmprogrammingID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                AmazonS3 s3 = new AmazonS3();
                s3 = AmazonS3Factory.setAmazonS3();

                lstEnrollmentDetails = new List<EnrollmentDetailListDto>();

                while (reader.Read())
                {
                    enrollmentDetail = new EnrollmentDetailListDto();
                    enrollmentDetail.enrollmentDetailID = reader.GetInt32(reader.GetOrdinal("enrollmentDetailID"));
                    enrollmentDetail.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    enrollmentDetail.studentID = reader.GetInt32(reader.GetOrdinal("studentID"));
                    enrollmentDetail.student_code = reader.GetString(reader.GetOrdinal("student_code"));
                    enrollmentDetail.student_name = reader.GetString(reader.GetOrdinal("student_name"));
                    enrollmentDetail.student_firstName = reader.GetString(reader.GetOrdinal("student_firstName"));
                    enrollmentDetail.student_lastName = reader.GetString(reader.GetOrdinal("student_lastName"));
                    enrollmentDetail.ImageKey = reader.GetString(reader.GetOrdinal("student_imageKey"));
                    enrollmentDetail.EndPoint = s3.getStudentEndPoint(enrollmentDetail.schoolID, enrollmentDetail.ImageKey);

                    lstEnrollmentDetails.Add(enrollmentDetail);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstEnrollmentDetails;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}

 
