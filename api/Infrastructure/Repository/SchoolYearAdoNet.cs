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



        public List<SchoolYearListDto> GetBycurrentYearByschoolIDByactive(Boolean isCurrentYear, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmcurrentYear = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                SchoolYearListDto schoolYear;
                List<SchoolYearListDto> lstSchoolYears;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetSchoolYearByschoolIDBycurrentYearByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmcurrentYear = new SqlParameter();
                prmcurrentYear.ParameterName = "@iscurrentYear";
                prmcurrentYear.SqlDbType = SqlDbType.Bit;
                prmcurrentYear.Value = isCurrentYear;
                command.Parameters.Add(prmcurrentYear);

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

                lstSchoolYears = new List<SchoolYearListDto>();

                while (reader.Read())
                {
                    schoolYear = new SchoolYearListDto();
                    schoolYear.schoolYearID = reader.GetInt32(reader.GetOrdinal("schoolYearID"));
                    schoolYear.year = reader.GetInt32(reader.GetOrdinal("year"));
                    lstSchoolYears.Add(schoolYear);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstSchoolYears;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
