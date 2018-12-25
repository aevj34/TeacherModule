using api.Application.Dto;
using api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure.Repository
{
    public class FolderAdoNetRepository
    {


        public Folder GetByfolderTypeIDBybucketIDByschoolIDByactive(Int32 folderTypeID, Int32 bucketID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmfolderTypeID = null;
            SqlParameter prmbucketID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                Folder folder;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetFolderByfolderTypeIDBybucketIDByschoolIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmfolderTypeID = new SqlParameter();
                prmfolderTypeID.ParameterName = "@folderTypeID";
                prmfolderTypeID.SqlDbType = SqlDbType.Int;
                prmfolderTypeID.Value = folderTypeID;
                command.Parameters.Add(prmfolderTypeID);

                prmbucketID = new SqlParameter();
                prmbucketID.ParameterName = "@bucketID";
                prmbucketID.SqlDbType = SqlDbType.Int;
                prmbucketID.Value = bucketID;
                command.Parameters.Add(prmbucketID);

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

                folder = new Folder();

                if (reader.Read())
                {
                    folder.folderID = reader.GetInt32(reader.GetOrdinal("folderID"));
                    folder.name = reader.GetString(reader.GetOrdinal("name"));
                    folder.noImage = reader.GetString(reader.GetOrdinal("noImage"));
                }

                command.Connection.Close();
                conn.Dispose();

                return folder;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}
