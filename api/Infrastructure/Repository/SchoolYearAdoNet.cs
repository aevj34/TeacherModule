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

    public class SchoolYearAdoNet: SchoolYearRepository
    {




        public SchoolYearListDto Obtain(Int32 schoolYearID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmschoolYearID = null;

            try
            {
                SchoolYearListDto schoolYear;
                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "ObtainSchoolYear";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmschoolYearID = new SqlParameter();
                prmschoolYearID.ParameterName = "@schoolYearID";
                prmschoolYearID.SqlDbType = SqlDbType.Int;
                prmschoolYearID.Value = schoolYearID;
                command.Parameters.Add(prmschoolYearID);

                command.Connection.Open();
                reader = command.ExecuteReader();


                schoolYear = new SchoolYearListDto();
                if (reader.Read())
                {
                    schoolYear.minimumNote = reader.GetInt32(reader.GetOrdinal("minimumNote"));
                }

                command.Connection.Close();
                conn.Dispose();

                return schoolYear;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
