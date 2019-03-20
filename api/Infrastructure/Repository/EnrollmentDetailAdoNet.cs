
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
                    enrollmentDetail.enrollmentID = reader.GetInt32(reader.GetOrdinal("enrollmentID"));
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

        public List<EnrollmentDetailListDto> GetStudentsByTutorID(Int32 schoolID, int tutorID, bool isCurrentPeriod, Boolean active, int skip, int pageSize)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String strSqlUser;
            SqlCommand cmdUser;
            SqlParameter prmtutorID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmIsCurrentPeriod = null;
            SqlParameter prmactive = null;
            SqlParameter prmskip = null;
            SqlParameter prmpageSize = null;

            try
            {
                    EnrollmentDetailListDto enrollmentDetail;
                    List<EnrollmentDetailListDto> lstEnrollmentDetails;

                    conn = new SqlConnection(Functions.GetConnectionString());

                strSqlUser = "GetStudentsByTutorID";

                cmdUser = new SqlCommand(strSqlUser, conn);
                cmdUser.CommandType = CommandType.StoredProcedure;

                prmtutorID = new SqlParameter();
                prmtutorID.ParameterName = "@tutorID";
                prmtutorID.SqlDbType = SqlDbType.Int;
                prmtutorID.Value = tutorID;
                cmdUser.Parameters.Add(prmtutorID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                cmdUser.Parameters.Add(prmschoolID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                cmdUser.Parameters.Add(prmactive);

                prmIsCurrentPeriod = new SqlParameter();
                prmIsCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmIsCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmIsCurrentPeriod.Value = isCurrentPeriod;
                cmdUser.Parameters.Add(prmIsCurrentPeriod);

                prmskip = new SqlParameter();
                prmskip.ParameterName = "@skip";
                prmskip.SqlDbType = SqlDbType.Int;
                prmskip.Value = skip;
                cmdUser.Parameters.Add(prmskip);

                prmpageSize = new SqlParameter();
                prmpageSize.ParameterName = "@pageSize";
                prmpageSize.SqlDbType = SqlDbType.Int;
                prmpageSize.Value = pageSize;
                cmdUser.Parameters.Add(prmpageSize);

                cmdUser.Connection.Open();
                reader = cmdUser.ExecuteReader();

                lstEnrollmentDetails = new List<EnrollmentDetailListDto>();
                AmazonS3 s3 = new AmazonS3();

                s3 = AmazonS3Factory.setAmazonS3();

                while (reader.Read())
                {
                    enrollmentDetail = new EnrollmentDetailListDto();
                    enrollmentDetail.studentID = reader.GetInt32(reader.GetOrdinal("studentID"));
                    enrollmentDetail.enrollmentID = reader.GetInt32(reader.GetOrdinal("enrollmentID"));
                    enrollmentDetail.student_code = reader.GetString(reader.GetOrdinal("student_code"));
                    enrollmentDetail.student_name = reader.GetString(reader.GetOrdinal("student_name"));
                    enrollmentDetail.student_firstName = reader.GetString(reader.GetOrdinal("student_firstName"));
                    enrollmentDetail.student_lastName = reader.GetString(reader.GetOrdinal("student_lastName"));

                    enrollmentDetail.ImageKey = reader.GetString(reader.GetOrdinal("student_imageKey"));
                    enrollmentDetail.EndPoint = s3.getStudentEndPoint(schoolID, enrollmentDetail.ImageKey);

                    lstEnrollmentDetails.Add(enrollmentDetail);

                }

                cmdUser.Connection.Close();
                conn.Dispose();

                return lstEnrollmentDetails;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public int GetStudentsByTutorIDCount(Int32 schoolID, int tutorID, bool isCurrentPeriod, Boolean active)
        {

            SqlConnection conn = null;
            String strSqlUser;
            SqlCommand cmdUser;
            SqlParameter prmtutorID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmIsCurrentPeriod = null;
            SqlParameter prmactive = null;

            try
            {

                conn = new SqlConnection(Functions.GetConnectionString());

                strSqlUser = "GetStudentsByTutorIDCount";

                cmdUser = new SqlCommand(strSqlUser, conn);
                cmdUser.CommandType = CommandType.StoredProcedure;

                prmtutorID = new SqlParameter();
                prmtutorID.ParameterName = "@tutorID";
                prmtutorID.SqlDbType = SqlDbType.Int;
                prmtutorID.Value = tutorID;
                cmdUser.Parameters.Add(prmtutorID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                cmdUser.Parameters.Add(prmschoolID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                cmdUser.Parameters.Add(prmactive);

                prmIsCurrentPeriod = new SqlParameter();
                prmIsCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmIsCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmIsCurrentPeriod.Value = isCurrentPeriod;
                cmdUser.Parameters.Add(prmIsCurrentPeriod);

                cmdUser.Connection.Open();

                int count = 0;
                count = Convert.ToInt32(cmdUser.ExecuteScalar());

                cmdUser.Connection.Close();
                conn.Dispose();

                return count;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public List<CourseDto> GetCourses(Int32 schoolID, Int32 enrollmentID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmenrollmentID = null;
            SqlParameter prmactive = null;

            try
            {
                CourseDto enrollmentDetail;
                List<CourseDto> lstEnrollmentDetails;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetEnrollmentDetailByschoolIDByenrollmentIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmenrollmentID = new SqlParameter();
                prmenrollmentID.ParameterName = "@enrollmentID";
                prmenrollmentID.SqlDbType = SqlDbType.Int;
                prmenrollmentID.Value = enrollmentID;
                command.Parameters.Add(prmenrollmentID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstEnrollmentDetails = new List<CourseDto>();

                while (reader.Read())
                {
                    enrollmentDetail = new CourseDto();
                    enrollmentDetail.credits = reader.GetInt32(reader.GetOrdinal("credits"));
                    enrollmentDetail.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    enrollmentDetail.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    enrollmentDetail.evaluationFormulaID = reader.GetInt32(reader.GetOrdinal("evaluationFormulaID"));
                    enrollmentDetail.course_name = reader.GetString(reader.GetOrdinal("course_name"));
                    enrollmentDetail.courseID = reader.GetInt32(reader.GetOrdinal("courseID"));
                    enrollmentDetail.course_grade = reader.GetInt32(reader.GetOrdinal("course_grade"));
                    enrollmentDetail.section_name = reader.GetString(reader.GetOrdinal("section_name"));
                    enrollmentDetail.turn_name = reader.GetString(reader.GetOrdinal("turn_name"));
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

 
