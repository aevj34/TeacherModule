
using api.Application.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using api.Domain.Repository;
namespace api.Infrastructure.Repository
{
    public class AssistanceDetailAdoNet : AssistanceDetailRepository
    {


        public List<AssistanceDetailDto> GetByprogrammingIDByschoolIDByactive(Int32 programmingID, Int32 schoolID, Boolean active)
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
                AssistanceDetailDto assistanceDetail;
                List<AssistanceDetailDto> lstAssistanceDetails;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetAssistanceDetailByprogrammingIDByschoolIDByactive";

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

                lstAssistanceDetails = new List<AssistanceDetailDto>();

                while (reader.Read())
                {
                    assistanceDetail = new AssistanceDetailDto();
                    assistanceDetail.assistanceDetailID = reader.GetInt32(reader.GetOrdinal("assistanceDetailID"));
                    assistanceDetail.assistanceID = reader.GetInt32(reader.GetOrdinal("assistanceID"));
                    assistanceDetail.assistanceTypeID = reader.GetInt32(reader.GetOrdinal("assistanceTypeID"));
                    assistanceDetail.studentID = reader.GetInt32(reader.GetOrdinal("studentID"));
                    assistanceDetail.assistanceType_name = reader.GetString(reader.GetOrdinal("assistanceType_name"));
                    assistanceDetail.assistanceType_abbreviation = reader.GetString(reader.GetOrdinal("assistanceType_abbreviation"));
                    lstAssistanceDetails.Add(assistanceDetail);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstAssistanceDetails;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        public List<AssistanceDetailDto> GetAssistanceDetailByStudent(Int32 enrollmentID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmenrollmentID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                AssistanceDetailDto assistanceDetail;
                List<AssistanceDetailDto> lstAssistanceDetails;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetAssistanceDetailByStudent";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmenrollmentID = new SqlParameter();
                prmenrollmentID.ParameterName = "@enrollmentID";
                prmenrollmentID.SqlDbType = SqlDbType.Int;
                prmenrollmentID.Value = enrollmentID;
                command.Parameters.Add(prmenrollmentID);

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

                lstAssistanceDetails = new List<AssistanceDetailDto>();

                while (reader.Read())
                {
                    assistanceDetail = new AssistanceDetailDto();
                    assistanceDetail.assistanceDetailID = reader.GetInt32(reader.GetOrdinal("assistanceDetailID"));
                    assistanceDetail.assistanceID = reader.GetInt32(reader.GetOrdinal("assistanceID"));
                    assistanceDetail.enrollmentID = reader.GetInt32(reader.GetOrdinal("enrollmentID"));
                    assistanceDetail.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    assistanceDetail.assistanceTypeID = reader.GetInt32(reader.GetOrdinal("assistanceTypeID"));
                    assistanceDetail.studentID = reader.GetInt32(reader.GetOrdinal("studentID"));
                    assistanceDetail.assistanceType_name = reader.GetString(reader.GetOrdinal("assistanceType_name"));
                    assistanceDetail.assistanceType_abbreviation = reader.GetString(reader.GetOrdinal("assistanceType_abbreviation"));
                    lstAssistanceDetails.Add(assistanceDetail);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstAssistanceDetails;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public void Update(AssistanceDetailDto assistanceDetailDto)

        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUassistanceDetailID;
            SqlParameter prmUassistanceID;
            SqlParameter prmUassistanceTypeID;
            SqlParameter prmUstudentID;
            SqlParameter prmUschoolID;
            SqlParameter prmUcareerID;
            SqlParameter prmUcourseID;
            SqlParameter prmUschoolYearID;
            SqlParameter prmUclassroomID;
            SqlParameter prmUheadquartersID;
            SqlParameter prmUprogrammingID;
            SqlParameter prmUteacherID;
            SqlParameter prmUturnID;
            SqlParameter prmUsectionID;
            SqlParameter prmUactive;
            SqlParameter prmUevaluationPeriodID;
            SqlParameter prmUenrollmentID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateAssistanceDetail";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUassistanceDetailID = new SqlParameter();
                prmUassistanceDetailID.ParameterName = "@assistanceDetailID";
                prmUassistanceDetailID.SqlDbType = SqlDbType.Int;
                prmUassistanceDetailID.Value = assistanceDetailDto.assistanceDetailID;
                command.Parameters.Add(prmUassistanceDetailID);

                prmUassistanceID = new SqlParameter();
                prmUassistanceID.ParameterName = "@assistanceID";
                prmUassistanceID.SqlDbType = SqlDbType.Int;
                prmUassistanceID.Value = assistanceDetailDto.assistanceID;
                command.Parameters.Add(prmUassistanceID);

                prmUassistanceTypeID = new SqlParameter();
                prmUassistanceTypeID.ParameterName = "@assistanceTypeID";
                prmUassistanceTypeID.SqlDbType = SqlDbType.Int;
                prmUassistanceTypeID.Value = assistanceDetailDto.assistanceTypeID;
                command.Parameters.Add(prmUassistanceTypeID);

                prmUstudentID = new SqlParameter();
                prmUstudentID.ParameterName = "@studentID";
                prmUstudentID.SqlDbType = SqlDbType.Int;
                prmUstudentID.Value = assistanceDetailDto.studentID;
                command.Parameters.Add(prmUstudentID);

                prmUschoolID = new SqlParameter();
                prmUschoolID.ParameterName = "@schoolID";
                prmUschoolID.SqlDbType = SqlDbType.Int;
                prmUschoolID.Value = assistanceDetailDto.schoolID;
                command.Parameters.Add(prmUschoolID);

                prmUcareerID = new SqlParameter();
                prmUcareerID.ParameterName = "@careerID";
                prmUcareerID.SqlDbType = SqlDbType.Int;
                prmUcareerID.Value = assistanceDetailDto.careerID;
                command.Parameters.Add(prmUcareerID);

                prmUcourseID = new SqlParameter();
                prmUcourseID.ParameterName = "@courseID";
                prmUcourseID.SqlDbType = SqlDbType.Int;
                prmUcourseID.Value = assistanceDetailDto.courseID;
                command.Parameters.Add(prmUcourseID);

                prmUschoolYearID = new SqlParameter();
                prmUschoolYearID.ParameterName = "@schoolYearID";
                prmUschoolYearID.SqlDbType = SqlDbType.Int;
                prmUschoolYearID.Value = assistanceDetailDto.schoolYearID;
                command.Parameters.Add(prmUschoolYearID);

                prmUclassroomID = new SqlParameter();
                prmUclassroomID.ParameterName = "@classroomID";
                prmUclassroomID.SqlDbType = SqlDbType.Int;
                prmUclassroomID.Value = assistanceDetailDto.classroomID;
                command.Parameters.Add(prmUclassroomID);

                prmUheadquartersID = new SqlParameter();
                prmUheadquartersID.ParameterName = "@headquartersID";
                prmUheadquartersID.SqlDbType = SqlDbType.Int;
                prmUheadquartersID.Value = assistanceDetailDto.headquartersID;
                command.Parameters.Add(prmUheadquartersID);

                prmUprogrammingID = new SqlParameter();
                prmUprogrammingID.ParameterName = "@programmingID";
                prmUprogrammingID.SqlDbType = SqlDbType.Int;
                prmUprogrammingID.Value = assistanceDetailDto.programmingID;
                command.Parameters.Add(prmUprogrammingID);

                prmUteacherID = new SqlParameter();
                prmUteacherID.ParameterName = "@teacherID";
                prmUteacherID.SqlDbType = SqlDbType.Int;
                prmUteacherID.Value = assistanceDetailDto.teacherID;
                command.Parameters.Add(prmUteacherID);

                prmUturnID = new SqlParameter();
                prmUturnID.ParameterName = "@turnID";
                prmUturnID.SqlDbType = SqlDbType.Int;
                prmUturnID.Value = assistanceDetailDto.turnID;
                command.Parameters.Add(prmUturnID);

                prmUsectionID = new SqlParameter();
                prmUsectionID.ParameterName = "@sectionID";
                prmUsectionID.SqlDbType = SqlDbType.Int;
                prmUsectionID.Value = assistanceDetailDto.sectionID;
                command.Parameters.Add(prmUsectionID);

                prmUactive = new SqlParameter();
                prmUactive.ParameterName = "@active";
                prmUactive.SqlDbType = SqlDbType.Bit;
                prmUactive.Value = assistanceDetailDto.active;
                command.Parameters.Add(prmUactive);

                prmUevaluationPeriodID = new SqlParameter();
                prmUevaluationPeriodID.ParameterName = "@evaluationPeriodID";
                prmUevaluationPeriodID.SqlDbType = SqlDbType.Int;
                prmUevaluationPeriodID.Value = assistanceDetailDto.evaluationPeriodID;
                command.Parameters.Add(prmUevaluationPeriodID);

                prmUenrollmentID = new SqlParameter();
                prmUenrollmentID.ParameterName = "@enrollmentID";
                prmUenrollmentID.SqlDbType = SqlDbType.Int;
                prmUenrollmentID.Value = assistanceDetailDto.enrollmentID;
                command.Parameters.Add(prmUenrollmentID);

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

        public Int32 Insert(AssistanceDetailDto assistanceDetail)

        {
            SqlConnection conn = null;
            String sqlAssistanceDetailInsert;
            SqlCommand cmdAssistanceDetailInsert;
            SqlParameter prmassistanceDetailID;
            SqlParameter prmassistanceID;
            SqlParameter prmassistanceTypeID;
            SqlParameter prmstudentID;
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
            SqlParameter prmenrollmentID;
            Int32 intassistanceDetailID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());

                sqlAssistanceDetailInsert = "InsertAssistanceDetail";

                cmdAssistanceDetailInsert = new SqlCommand(sqlAssistanceDetailInsert, conn);
                cmdAssistanceDetailInsert.CommandType = CommandType.StoredProcedure;

                prmassistanceDetailID = new SqlParameter();
                prmassistanceDetailID.Direction = ParameterDirection.ReturnValue;
                prmassistanceDetailID.SqlDbType = SqlDbType.Int;
                cmdAssistanceDetailInsert.Parameters.Add(prmassistanceDetailID);

                prmassistanceID = new SqlParameter();
                prmassistanceID.ParameterName = "@assistanceID";
                prmassistanceID.SqlDbType = SqlDbType.Int;
                prmassistanceID.Value = assistanceDetail.assistanceID;
                cmdAssistanceDetailInsert.Parameters.Add(prmassistanceID);

                prmassistanceTypeID = new SqlParameter();
                prmassistanceTypeID.ParameterName = "@assistanceTypeID";
                prmassistanceTypeID.SqlDbType = SqlDbType.Int;
                prmassistanceTypeID.Value = assistanceDetail.assistanceTypeID;
                cmdAssistanceDetailInsert.Parameters.Add(prmassistanceTypeID);

                prmstudentID = new SqlParameter();
                prmstudentID.ParameterName = "@studentID";
                prmstudentID.SqlDbType = SqlDbType.Int;
                prmstudentID.Value = assistanceDetail.studentID;
                cmdAssistanceDetailInsert.Parameters.Add(prmstudentID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = assistanceDetail.schoolID;
                cmdAssistanceDetailInsert.Parameters.Add(prmschoolID);

                prmcareerID = new SqlParameter();
                prmcareerID.ParameterName = "@careerID";
                prmcareerID.SqlDbType = SqlDbType.Int;
                prmcareerID.Value = assistanceDetail.careerID;
                cmdAssistanceDetailInsert.Parameters.Add(prmcareerID);

                prmcourseID = new SqlParameter();
                prmcourseID.ParameterName = "@courseID";
                prmcourseID.SqlDbType = SqlDbType.Int;
                prmcourseID.Value = assistanceDetail.courseID;
                cmdAssistanceDetailInsert.Parameters.Add(prmcourseID);

                prmschoolYearID = new SqlParameter();
                prmschoolYearID.ParameterName = "@schoolYearID";
                prmschoolYearID.SqlDbType = SqlDbType.Int;
                prmschoolYearID.Value = assistanceDetail.schoolYearID;
                cmdAssistanceDetailInsert.Parameters.Add(prmschoolYearID);

                prmclassroomID = new SqlParameter();
                prmclassroomID.ParameterName = "@classroomID";
                prmclassroomID.SqlDbType = SqlDbType.Int;
                prmclassroomID.Value = assistanceDetail.classroomID;
                cmdAssistanceDetailInsert.Parameters.Add(prmclassroomID);

                prmheadquartersID = new SqlParameter();
                prmheadquartersID.ParameterName = "@headquartersID";
                prmheadquartersID.SqlDbType = SqlDbType.Int;
                prmheadquartersID.Value = assistanceDetail.headquartersID;
                cmdAssistanceDetailInsert.Parameters.Add(prmheadquartersID);

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = assistanceDetail.programmingID;
                cmdAssistanceDetailInsert.Parameters.Add(prmprogrammingID);

                prmteacherID = new SqlParameter();
                prmteacherID.ParameterName = "@teacherID";
                prmteacherID.SqlDbType = SqlDbType.Int;
                prmteacherID.Value = assistanceDetail.teacherID;
                cmdAssistanceDetailInsert.Parameters.Add(prmteacherID);

                prmturnID = new SqlParameter();
                prmturnID.ParameterName = "@turnID";
                prmturnID.SqlDbType = SqlDbType.Int;
                prmturnID.Value = assistanceDetail.turnID;
                cmdAssistanceDetailInsert.Parameters.Add(prmturnID);

                prmsectionID = new SqlParameter();
                prmsectionID.ParameterName = "@sectionID";
                prmsectionID.SqlDbType = SqlDbType.Int;
                prmsectionID.Value = assistanceDetail.sectionID;
                cmdAssistanceDetailInsert.Parameters.Add(prmsectionID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = assistanceDetail.active;
                cmdAssistanceDetailInsert.Parameters.Add(prmactive);

                prmevaluationPeriodID = new SqlParameter();
                prmevaluationPeriodID.ParameterName = "@evaluationPeriodID";
                prmevaluationPeriodID.SqlDbType = SqlDbType.Int;
                prmevaluationPeriodID.Value = assistanceDetail.evaluationPeriodID;
                cmdAssistanceDetailInsert.Parameters.Add(prmevaluationPeriodID);

                prmenrollmentID = new SqlParameter();
                prmenrollmentID.ParameterName = "@enrollmentID";
                prmenrollmentID.SqlDbType = SqlDbType.Int;
                prmenrollmentID.Value = assistanceDetail.enrollmentID;
                cmdAssistanceDetailInsert.Parameters.Add(prmenrollmentID);

                cmdAssistanceDetailInsert.Connection.Open();
                cmdAssistanceDetailInsert.ExecuteNonQuery();

                intassistanceDetailID = Convert.ToInt32(prmassistanceDetailID.Value);

                cmdAssistanceDetailInsert.Connection.Close();
                conn.Dispose();

                return intassistanceDetailID;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

    }
}

