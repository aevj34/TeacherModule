
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
 public class ProgrammingAdoNet:ProgrammingRepository
 {


        public List<ProgrammingListDto> getCurrentCoursesByTeacherID(Int32 schoolID, Int32 teacherID, Boolean active, Boolean isCurrentPeriod)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmisCurrentPeriod = null;
            SqlParameter prmteacherID = null;
            SqlParameter prmactive = null;

            try
            {
                ProgrammingListDto programming;
                List<ProgrammingListDto> lstProgrammings;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetCurrentCourses";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmisCurrentPeriod = new SqlParameter();
                prmisCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmisCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmisCurrentPeriod.Value = isCurrentPeriod;
                command.Parameters.Add(prmisCurrentPeriod);

                prmteacherID = new SqlParameter();
                prmteacherID.ParameterName = "@teacherID";
                prmteacherID.SqlDbType = SqlDbType.Int;
                prmteacherID.Value = teacherID;
                command.Parameters.Add(prmteacherID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstProgrammings = new List<ProgrammingListDto>();

                while (reader.Read())
                {
                    programming = new ProgrammingListDto();
                    programming.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    programming.evaluationFormulaID = reader.GetInt32(reader.GetOrdinal("evaluationFormulaID"));
                    programming.career_name = reader.GetString(reader.GetOrdinal("career_name"));
                    programming.schoolID = schoolID;
                    programming.classroom_name = reader.GetString(reader.GetOrdinal("classroom_name"));
                    programming.course_name = reader.GetString(reader.GetOrdinal("course_name"));
                    programming.course_credits = reader.GetInt32(reader.GetOrdinal("course_credits"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.evaluationPeriod_name = reader.GetString(reader.GetOrdinal("evaluationPeriod_name"));
                    programming.section_name = reader.GetInt32(reader.GetOrdinal("grade")) + "" + reader.GetString(reader.GetOrdinal("section_name"));
                    programming.turn_name = reader.GetString(reader.GetOrdinal("turn_name"));

                    programming.courseID = reader.GetInt32(reader.GetOrdinal("courseID"));
                    programming.schoolYearID = reader.GetInt32(reader.GetOrdinal("schoolYearID"));
                    programming.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    programming.careerID = reader.GetInt32(reader.GetOrdinal("careerID"));
                    programming.sectionID = reader.GetInt32(reader.GetOrdinal("sectionID"));
                    programming.turnID = reader.GetInt32(reader.GetOrdinal("turnID"));
                    programming.periodTypeID = reader.GetInt32(reader.GetOrdinal("periodTypeID"));
                    programming.evaluationPeriodID = reader.GetInt32(reader.GetOrdinal("evaluationPeriodID"));
                    programming.headquartersID = reader.GetInt32(reader.GetOrdinal("headquartersID"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.teacherID = teacherID;

                    lstProgrammings.Add(programming);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstProgrammings;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public List<ProgrammingListDto> getCurrentCoursesByTutorID(Int32 schoolID, Int32 tutorID, Boolean active, Boolean isCurrentPeriod)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmisCurrentPeriod = null;
            SqlParameter prmtutorID = null;
            SqlParameter prmactive = null;

            try
            {
                ProgrammingListDto programming;
                List<ProgrammingListDto> lstProgrammings;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetCurrentCoursesByTutorID";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmisCurrentPeriod = new SqlParameter();
                prmisCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmisCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmisCurrentPeriod.Value = isCurrentPeriod;
                command.Parameters.Add(prmisCurrentPeriod);

                prmtutorID = new SqlParameter();
                prmtutorID.ParameterName = "@tutorID";
                prmtutorID.SqlDbType = SqlDbType.Int;
                prmtutorID.Value = tutorID;
                command.Parameters.Add(prmtutorID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                AmazonS3 s3 = new AmazonS3();
                s3 = AmazonS3Factory.setAmazonS3();

                lstProgrammings = new List<ProgrammingListDto>();

                while (reader.Read())
                {
                    programming = new ProgrammingListDto();
                    programming.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    programming.evaluationFormulaID = reader.GetInt32(reader.GetOrdinal("evaluationFormulaID"));
                    programming.career_name = reader.GetString(reader.GetOrdinal("career_name"));
                    programming.schoolID = schoolID;
                    programming.classroom_name = reader.GetString(reader.GetOrdinal("classroom_name"));
                    programming.course_name = reader.GetString(reader.GetOrdinal("course_name"));
                    programming.course_credits = reader.GetInt32(reader.GetOrdinal("course_credits"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.evaluationPeriod_name = reader.GetString(reader.GetOrdinal("evaluationPeriod_name"));
                    programming.section_name = reader.GetInt32(reader.GetOrdinal("grade")) + ""+ reader.GetString(reader.GetOrdinal("section_name"));
                    programming.turn_name = reader.GetString(reader.GetOrdinal("turn_name"));

                    programming.courseID = reader.GetInt32(reader.GetOrdinal("courseID"));
                    programming.schoolYearID = reader.GetInt32(reader.GetOrdinal("schoolYearID"));
                    programming.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    programming.careerID = reader.GetInt32(reader.GetOrdinal("careerID"));
                    programming.sectionID = reader.GetInt32(reader.GetOrdinal("sectionID"));
                    programming.turnID = reader.GetInt32(reader.GetOrdinal("turnID"));
                    programming.periodTypeID = reader.GetInt32(reader.GetOrdinal("periodTypeID"));
                    programming.evaluationPeriodID = reader.GetInt32(reader.GetOrdinal("evaluationPeriodID"));
                    programming.headquartersID = reader.GetInt32(reader.GetOrdinal("headquartersID"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.teacherID = reader.GetInt32(reader.GetOrdinal("teacherID")); ;

                    programming.imageKey = reader.GetString(reader.GetOrdinal("imageKey"));
                    programming.endPoint = s3.getTeacherEndPoint(programming.schoolID, programming.imageKey);
                    programming.teacher_name = reader.GetString(reader.GetOrdinal("teacher_name"));
                    programming.teacher_firstName = reader.GetString(reader.GetOrdinal("teacher_firstName"));
                    programming.teacher_lastName = reader.GetString(reader.GetOrdinal("teacher_lastName"));

                    lstProgrammings.Add(programming);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstProgrammings;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        public List<ProgrammingListDto1> getCurrentCoursesByTeacherIDToAssistance(Int32 schoolID, Int32 teacherID, Boolean active, Boolean isCurrentPeriod)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmisCurrentPeriod = null;
            SqlParameter prmteacherID = null;
            SqlParameter prmactive = null;

            try
            {
                ProgrammingListDto1 programming;
                List<ProgrammingListDto1> lstProgrammings;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetCurrentCoursesToAssistance";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmisCurrentPeriod = new SqlParameter();
                prmisCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmisCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmisCurrentPeriod.Value = isCurrentPeriod;
                command.Parameters.Add(prmisCurrentPeriod);

                prmteacherID = new SqlParameter();
                prmteacherID.ParameterName = "@teacherID";
                prmteacherID.SqlDbType = SqlDbType.Int;
                prmteacherID.Value = teacherID;
                command.Parameters.Add(prmteacherID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstProgrammings = new List<ProgrammingListDto1>();

                while (reader.Read())
                {
                    programming = new ProgrammingListDto1();
                    programming.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    programming.career_name = reader.GetString(reader.GetOrdinal("career_name"));
                    programming.schoolID = schoolID;
                    programming.course_name = reader.GetString(reader.GetOrdinal("course_name"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.evaluationPeriod_name = reader.GetString(reader.GetOrdinal("evaluationPeriod_name"));
                    programming.section_name = reader.GetString(reader.GetOrdinal("section_name"));
                    programming.turn_name = reader.GetString(reader.GetOrdinal("turn_name"));

                    lstProgrammings.Add(programming);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstProgrammings;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        public ProgrammingDto Obtain(Int32 programmingID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmprogrammingID = null;

            try
            {
                ProgrammingDto programming;
                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "ObtainProgramming";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = programmingID;
                command.Parameters.Add(prmprogrammingID);

                command.Connection.Open();
                reader = command.ExecuteReader();


                programming = new ProgrammingDto();
                if (reader.Read())
                {
                    programming.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    programming.teacherID = reader.GetInt32(reader.GetOrdinal("teacherID"));
                    programming.classroomID = reader.GetInt32(reader.GetOrdinal("classroomID"));
                    programming.courseID = reader.GetInt32(reader.GetOrdinal("courseID"));
                    programming.schoolYearID = reader.GetInt32(reader.GetOrdinal("schoolYearID"));
                    programming.schoolID = reader.GetInt32(reader.GetOrdinal("schoolID"));
                    programming.careerID = reader.GetInt32(reader.GetOrdinal("careerID"));
                    programming.sectionID = reader.GetInt32(reader.GetOrdinal("sectionID"));
                    programming.turnID = reader.GetInt32(reader.GetOrdinal("turnID"));
                    programming.evaluationFormulaID = reader.GetInt32(reader.GetOrdinal("evaluationFormulaID"));
                    programming.vacant = reader.GetInt32(reader.GetOrdinal("vacant"));
                    programming.periodTypeID = reader.GetInt32(reader.GetOrdinal("periodTypeID"));
                    programming.evaluationPeriodID = reader.GetInt32(reader.GetOrdinal("evaluationPeriodID"));
                    programming.headquartersID = reader.GetInt32(reader.GetOrdinal("headquartersID"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.active = reader.GetBoolean(reader.GetOrdinal("active"));
                }

                command.Connection.Close();
                conn.Dispose();

                return programming;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



        public ProgrammingDto ObtainPDF(Int32 programmingID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmprogrammingID = null;

            try
            {
                ProgrammingDto programming;
                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "ObtainProgrammingPDF";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = programmingID;
                command.Parameters.Add(prmprogrammingID);

                command.Connection.Open();
                reader = command.ExecuteReader();


                programming = new ProgrammingDto();
                if (reader.Read())
                {
                    programming.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    programming.grade = reader.GetInt32(reader.GetOrdinal("grade"));
                    programming.career_name = reader.GetString(reader.GetOrdinal("career_name"));
                    programming.course_name = reader.GetString(reader.GetOrdinal("course_name"));
                    programming.school_name = reader.GetString(reader.GetOrdinal("school_name"));
                    programming.section_name = reader.GetString(reader.GetOrdinal("section_name"));
                    programming.teacher_name = reader.GetString(reader.GetOrdinal("teacher_name"));
                    programming.teacher_firstName = reader.GetString(reader.GetOrdinal("teacher_firstName"));
                    programming.teacher_lastName = reader.GetString(reader.GetOrdinal("teacher_lastName"));
                    programming.turn_name = reader.GetString(reader.GetOrdinal("turn_name"));
                    programming.classesNumber = reader.GetInt32(reader.GetOrdinal("classesNumber"));
                }

                command.Connection.Close();
                conn.Dispose();

                return programming;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}

 
