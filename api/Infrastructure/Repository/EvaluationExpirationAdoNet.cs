
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
using api.Common.Infrastructure.Security;

namespace api.Infrastructure.Repository
 {
 public class EvaluationExpirationAdoNet:EvaluationExpirationRepository
 {


        public List<EvaluationExpirationListDto> GetByprogrammingIDByactive(Int32 programmingID, bool active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmprogrammingID = null;
            SqlParameter prmactive = null;

            try
            {
                EvaluationExpirationListDto evaluationExpiration;
                List<EvaluationExpirationListDto> lstEvaluationExpirations;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetEvaluationExpirationByprogrammingIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

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

                lstEvaluationExpirations = new List<EvaluationExpirationListDto>();

                while (reader.Read())
                {
                    evaluationExpiration = new EvaluationExpirationListDto();
                    evaluationExpiration.startDate = reader.GetDateTime(reader.GetOrdinal("startDate"));
                    evaluationExpiration.endDate = reader.GetDateTime(reader.GetOrdinal("endDate"));
                    evaluationExpiration.evaluationExpirationID = reader.GetInt32(reader.GetOrdinal("evaluationExpirationID"));
                    evaluationExpiration.startDateShow = TimerAgo.TimeShow(evaluationExpiration.startDate, Formater.ShortDateTime());
                    evaluationExpiration.startDateAgo = TimerAgo.TimeAgo(evaluationExpiration.startDate);
                    evaluationExpiration.endDateShow = TimerAgo.TimeShow(evaluationExpiration.endDate, Formater.ShortDateTime());
                    evaluationExpiration.endDateAgo = TimerAgo.TimeAgo(evaluationExpiration.endDate);
                    evaluationExpiration.programmingID = reader.GetInt32(reader.GetOrdinal("programmingID"));
                    evaluationExpiration.evaluationID = reader.GetInt32(reader.GetOrdinal("evaluationID"));

                 

                    lstEvaluationExpirations.Add(evaluationExpiration);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstEvaluationExpirations;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


    }
}

 
