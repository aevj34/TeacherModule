
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
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


    }
}

 
