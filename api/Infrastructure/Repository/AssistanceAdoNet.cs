
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
using api.Common.Infrastructure.Security;
using api.Domain.Entity;

namespace api.Infrastructure.Repository
 {
 public class AssistanceAdoNet:AssistanceRepository
 {


        public List<AssistanceListDto> GetLessons(Int32 schoolID, Int32 programmingID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmprogrammingID = null;
            SqlParameter prmactive = null;

            try
            {
                AssistanceListDto assistance;
                List<AssistanceListDto> lstAssistances;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetAssistanceByschoolIDByprogrammingIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = programmingID;
                command.Parameters.Add(prmprogrammingID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstAssistances = new List<AssistanceListDto>();

                while (reader.Read())
                {
                    assistance = new AssistanceListDto();
                    assistance.assistanceID = reader.GetInt32(reader.GetOrdinal("assistanceID"));
                    assistance.classTheme = reader.GetString(reader.GetOrdinal("classTheme"));
                    assistance.dateClass = reader.GetDateTime(reader.GetOrdinal("dateClass"));
                    assistance.dateClassShow = TimerAgo.TimeShow(assistance.dateClass, Formater.SortableDateTime());
                    assistance.dateClassAgo = assistance.dateClass.Day + " " + Formater.getShortName(assistance.dateClass.Month.ToString());
                    lstAssistances.Add(assistance);
           

     }

                command.Connection.Close();
                conn.Dispose();

                return lstAssistances;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }




        public Int32 Insert(Assistance assistance)

        {
            SqlConnection conn = null;
            String sqlAssistanceInsert;
            SqlCommand cmdAssistanceInsert;
            SqlParameter prmassistanceID;
            SqlParameter prmclassTheme;
            SqlParameter prmdateClass;
            SqlParameter prmobservation;
            SqlParameter prmschoolID;
            SqlParameter prmcareerID;
            SqlParameter prmcourseID;
            SqlParameter prmschoolYearID;
            SqlParameter prmclassroomID;
            SqlParameter prmheadquartersID;
            SqlParameter prmprogrammingID;
            SqlParameter prmteacherID;
            SqlParameter prmturnID;
            SqlParameter prmsectionID;
            SqlParameter prmactive;
            SqlParameter prmevaluationPeriodID;
            Int32 intassistanceID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());

                sqlAssistanceInsert = "InsertAssistance";

                cmdAssistanceInsert = new SqlCommand(sqlAssistanceInsert, conn);
                cmdAssistanceInsert.CommandType = CommandType.StoredProcedure;

                prmassistanceID = new SqlParameter();
                prmassistanceID.Direction = ParameterDirection.ReturnValue;
                prmassistanceID.SqlDbType = SqlDbType.Int;
                cmdAssistanceInsert.Parameters.Add(prmassistanceID);

                prmclassTheme = new SqlParameter();
                prmclassTheme.ParameterName = "@classTheme";
                prmclassTheme.SqlDbType = SqlDbType.VarChar;
                prmclassTheme.Value = assistance.classTheme;
                cmdAssistanceInsert.Parameters.Add(prmclassTheme);

                prmdateClass = new SqlParameter();
                prmdateClass.ParameterName = "@dateClass";
                prmdateClass.SqlDbType = SqlDbType.DateTime;
                prmdateClass.Value = assistance.dateClass;
                cmdAssistanceInsert.Parameters.Add(prmdateClass);

                prmobservation = new SqlParameter();
                prmobservation.ParameterName = "@observation";
                prmobservation.SqlDbType = SqlDbType.VarChar;
                prmobservation.Value = assistance.observation;
                cmdAssistanceInsert.Parameters.Add(prmobservation);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = assistance.schoolID;
                cmdAssistanceInsert.Parameters.Add(prmschoolID);

                prmcareerID = new SqlParameter();
                prmcareerID.ParameterName = "@careerID";
                prmcareerID.SqlDbType = SqlDbType.Int;
                prmcareerID.Value = assistance.careerID;
                cmdAssistanceInsert.Parameters.Add(prmcareerID);

                prmcourseID = new SqlParameter();
                prmcourseID.ParameterName = "@courseID";
                prmcourseID.SqlDbType = SqlDbType.Int;
                prmcourseID.Value = assistance.courseID;
                cmdAssistanceInsert.Parameters.Add(prmcourseID);

                prmschoolYearID = new SqlParameter();
                prmschoolYearID.ParameterName = "@schoolYearID";
                prmschoolYearID.SqlDbType = SqlDbType.Int;
                prmschoolYearID.Value = assistance.schoolYearID;
                cmdAssistanceInsert.Parameters.Add(prmschoolYearID);

                prmclassroomID = new SqlParameter();
                prmclassroomID.ParameterName = "@classroomID";
                prmclassroomID.SqlDbType = SqlDbType.Int;
                prmclassroomID.Value = assistance.classroomID;
                cmdAssistanceInsert.Parameters.Add(prmclassroomID);

                prmheadquartersID = new SqlParameter();
                prmheadquartersID.ParameterName = "@headquartersID";
                prmheadquartersID.SqlDbType = SqlDbType.Int;
                prmheadquartersID.Value = assistance.headquartersID;
                cmdAssistanceInsert.Parameters.Add(prmheadquartersID);

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = assistance.programmingID;
                cmdAssistanceInsert.Parameters.Add(prmprogrammingID);

                prmteacherID = new SqlParameter();
                prmteacherID.ParameterName = "@teacherID";
                prmteacherID.SqlDbType = SqlDbType.Int;
                prmteacherID.Value = assistance.teacherID;
                cmdAssistanceInsert.Parameters.Add(prmteacherID);

                prmturnID = new SqlParameter();
                prmturnID.ParameterName = "@turnID";
                prmturnID.SqlDbType = SqlDbType.Int;
                prmturnID.Value = assistance.turnID;
                cmdAssistanceInsert.Parameters.Add(prmturnID);

                prmsectionID = new SqlParameter();
                prmsectionID.ParameterName = "@sectionID";
                prmsectionID.SqlDbType = SqlDbType.Int;
                prmsectionID.Value = assistance.sectionID;
                cmdAssistanceInsert.Parameters.Add(prmsectionID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = assistance.active;
                cmdAssistanceInsert.Parameters.Add(prmactive);

                prmevaluationPeriodID = new SqlParameter();
                prmevaluationPeriodID.ParameterName = "@evaluationPeriodID";
                prmevaluationPeriodID.SqlDbType = SqlDbType.Int;
                prmevaluationPeriodID.Value = assistance.evaluationPeriodID;
                cmdAssistanceInsert.Parameters.Add(prmevaluationPeriodID);

                cmdAssistanceInsert.Connection.Open();
                cmdAssistanceInsert.ExecuteNonQuery();

                intassistanceID = Convert.ToInt32(prmassistanceID.Value);

                cmdAssistanceInsert.Connection.Close();
                conn.Dispose();

                return intassistanceID;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        public void Update(Assistance assistanceDto)

        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUassistanceID;
            SqlParameter prmUclassTheme;
            SqlParameter prmUdateClass;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateAssistance";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUassistanceID = new SqlParameter();
                prmUassistanceID.ParameterName = "@assistanceID";
                prmUassistanceID.SqlDbType = SqlDbType.Int;
                prmUassistanceID.Value = assistanceDto.assistanceID;
                command.Parameters.Add(prmUassistanceID);

                prmUclassTheme = new SqlParameter();
                prmUclassTheme.ParameterName = "@classTheme";
                prmUclassTheme.SqlDbType = SqlDbType.VarChar;
                prmUclassTheme.Value = assistanceDto.classTheme;
                command.Parameters.Add(prmUclassTheme);

                if (isDate(assistanceDto.dateClass))
                {
                    prmUdateClass = new SqlParameter();
                    prmUdateClass.ParameterName = "@dateClass";
                    prmUdateClass.SqlDbType = SqlDbType.DateTime;
                    prmUdateClass.Value = assistanceDto.dateClass;
                    command.Parameters.Add(prmUdateClass);
                }
               
                command.Connection.Open();
                command.ExecuteNonQuery();

                command.Connection.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public void UpdateByassistanceID(Int32 assistanceID_Filter, Boolean active)
        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUassistanceID_Filter;
            SqlParameter prmUactive;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateAssistanceByassistanceID";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUassistanceID_Filter = new SqlParameter();
                prmUassistanceID_Filter.ParameterName = "@assistanceID_Filter";
                prmUassistanceID_Filter.SqlDbType = SqlDbType.Int;
                prmUassistanceID_Filter.Value = assistanceID_Filter;
                command.Parameters.Add(prmUassistanceID_Filter);

                prmUactive = new SqlParameter();
                prmUactive.ParameterName = "@active";
                prmUactive.SqlDbType = SqlDbType.Bit;
                prmUactive.Value = active;
                command.Parameters.Add(prmUactive);

                command.Connection.Open();
                command.ExecuteNonQuery();

                command.Connection.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        protected bool isDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

 
