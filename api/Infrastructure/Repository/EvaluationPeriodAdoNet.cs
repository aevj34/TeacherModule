using api.Application.Dto;
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
    public class EvaluationPeriodAdoNet:EvaluationPeriodRepository
    {


        public List<EvaluationPeriodListDto> GetByactiveByisCurrentPeriodByschoolIDByschoolYearID(Boolean active, Boolean isCurrentPeriod, Int32 schoolID, Int32 schoolYearID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmactive = null;
            SqlParameter prmisCurrentPeriod = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmschoolYearID = null;

            try
            {
                EvaluationPeriodListDto evaluationPeriod;
                List<EvaluationPeriodListDto> lstEvaluationPeriods;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetEvaluationPeriodByactiveByisCurrentPeriodByschoolIDByschoolYearID";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                prmisCurrentPeriod = new SqlParameter();
                prmisCurrentPeriod.ParameterName = "@isCurrentPeriod";
                prmisCurrentPeriod.SqlDbType = SqlDbType.Bit;
                prmisCurrentPeriod.Value = isCurrentPeriod;
                command.Parameters.Add(prmisCurrentPeriod);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmschoolYearID = new SqlParameter();
                prmschoolYearID.ParameterName = "@schoolYearID";
                prmschoolYearID.SqlDbType = SqlDbType.Int;
                prmschoolYearID.Value = schoolYearID;
                command.Parameters.Add(prmschoolYearID);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstEvaluationPeriods = new List<EvaluationPeriodListDto>();

                while (reader.Read())
                {
                    evaluationPeriod = new EvaluationPeriodListDto();
                    evaluationPeriod.evaluationPeriodID = reader.GetInt32(reader.GetOrdinal("evaluationPeriodID"));
                    lstEvaluationPeriods.Add(evaluationPeriod);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstEvaluationPeriods;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
