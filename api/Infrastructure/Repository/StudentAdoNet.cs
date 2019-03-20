
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
 public class StudentAdoNet:StudentRepository
 {

        public StudentListDto GetBystudentIDByactive(Int32 studentID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmstudentID = null;
            SqlParameter prmactive = null;

            try
            {
                StudentListDto student;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetStudentBystudentIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmstudentID = new SqlParameter();
                prmstudentID.ParameterName = "@studentID";
                prmstudentID.SqlDbType = SqlDbType.Int;
                prmstudentID.Value = studentID;
                command.Parameters.Add(prmstudentID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                student = new StudentListDto();
                AmazonS3 s3 = new AmazonS3();
                s3 = AmazonS3Factory.setAmazonS3();

                while (reader.Read())
                {
                    student.code = reader.GetString(reader.GetOrdinal("code"));
                    student.name = reader.GetString(reader.GetOrdinal("name"));
                    student.otherName = reader.GetString(reader.GetOrdinal("otherName"));
                    student.firstName = reader.GetString(reader.GetOrdinal("firstName"));
                    student.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                    student.imageKey = reader.GetString(reader.GetOrdinal("imageKey"));
                    student.dni = reader.GetString(reader.GetOrdinal("dni"));
                    student.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    student.phone = reader.GetString(reader.GetOrdinal("phone"));
                    student.email = reader.GetString(reader.GetOrdinal("email"));
                    student.alternativeMail = reader.GetString(reader.GetOrdinal("alternativeMail"));
                    student.homeAddress = reader.GetString(reader.GetOrdinal("homeAddress"));
                    student.career_name = reader.GetString(reader.GetOrdinal("career_name"));
                    student.endPoint = s3.getStudentEndPoint(student.schoolID, student.imageKey);

                }

                command.Connection.Close();
                conn.Dispose();

                return student;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


    }
}

 
