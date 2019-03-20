
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
 namespace api.Infrastructure.Repository
 {
 public class AssistanceTypeAdoNet:AssistanceTypeRepository
 {


        public List<AssistanceTypeListDto> GetByschoolIDByactive(Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                AssistanceTypeListDto assistanceType;
                List<AssistanceTypeListDto> lstAssistanceTypes;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetAssistanceTypeByschoolIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

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

                lstAssistanceTypes = new List<AssistanceTypeListDto>();

                while (reader.Read())
                {
                    assistanceType = new AssistanceTypeListDto();
                    assistanceType.assistanceTypeID = reader.GetInt32(reader.GetOrdinal("assistanceTypeID"));
                    assistanceType.name = reader.GetString(reader.GetOrdinal("name"));
                    assistanceType.abbreviation = reader.GetString(reader.GetOrdinal("abbreviation"));
                    assistanceType.isLack = reader.GetBoolean(reader.GetOrdinal("isLack"));
                    lstAssistanceTypes.Add(assistanceType);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstAssistanceTypes;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


    }
}

 
