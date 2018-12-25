using api.Application.Dto;
using api.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using api.Domain.Entity;

namespace api.Infrastructure.Repository
{
    public class RoleInActionAdoNet : RoleInActionRepository
    {


        public List<RoleInActionListDto> GetByroleIDByschoolIDByactive(Int32 roleID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmroleID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                RoleInActionListDto roleInAction;
                List<RoleInActionListDto> lstRoleInActions;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetRoleInActionByroleIDByschoolIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmroleID = new SqlParameter();
                prmroleID.ParameterName = "@roleID";
                prmroleID.SqlDbType = SqlDbType.Int;
                prmroleID.Value = roleID;
                command.Parameters.Add(prmroleID);

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

                lstRoleInActions = new List<RoleInActionListDto>();

                while (reader.Read())
                {
                    roleInAction = new RoleInActionListDto();
                    roleInAction.actionID = reader.GetInt32(reader.GetOrdinal("actionID"));
                    roleInAction.orderNumber = reader.GetInt32(reader.GetOrdinal("orderNumber"));
                    roleInAction.action_name = reader.GetString(reader.GetOrdinal("action_name"));
                    roleInAction.action_entryURL = reader.GetString(reader.GetOrdinal("action_entryURL"));
                    roleInAction.action_imageName = reader.GetString(reader.GetOrdinal("action_imageName"));
                    roleInAction.action_imageInvert = reader.GetString(reader.GetOrdinal("action_imageInvert"));
                    roleInAction.menu_name = reader.GetString(reader.GetOrdinal("menu_name"));
                    roleInAction.menu_description = reader.GetString(reader.GetOrdinal("menu_description"));
                    roleInAction.module_name = reader.GetString(reader.GetOrdinal("module_name"));
                    lstRoleInActions.Add(roleInAction);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstRoleInActions;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



        public List<RoleInActionListDto> GetByRoleIDByactive(Int32 RoleID, Boolean active,int schoolID)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmRoleID = null;
            SqlParameter prmactive = null;
            SqlParameter prmSchoolID = null;
            try
            {
                RoleInActionListDto RoleInAction;
                List<RoleInActionListDto> lstRoleInActions;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetRoleInActionByRoleIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmRoleID = new SqlParameter();
                prmRoleID.ParameterName = "@RoleID";
                prmRoleID.SqlDbType = SqlDbType.Int;
                prmRoleID.Value = RoleID;
                command.Parameters.Add(prmRoleID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                prmSchoolID = new SqlParameter();
                prmSchoolID.ParameterName = "@SchoolID";
                prmSchoolID.SqlDbType = SqlDbType.Int;
                prmSchoolID.Value = schoolID;
                command.Parameters.Add(prmSchoolID);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstRoleInActions = new List<RoleInActionListDto>();

                while (reader.Read())
                {
                    RoleInAction = new RoleInActionListDto();
                    RoleInAction.roleInActionID = reader.GetInt32(reader.GetOrdinal("RoleInActionID"));
                    RoleInAction.actionID = reader.GetInt32(reader.GetOrdinal("ActionID"));
                    lstRoleInActions.Add(RoleInAction);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstRoleInActions;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public int GetByActionIDByRoleIDCount(Int32 ActionID, Int32 RoleID, int schoolID)
        {

            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmActionID = null;
            SqlParameter prmRoleID = null;
            SqlParameter prmSchoolID = null;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetRoleInActionByActionIDByRoleIDCount";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmActionID = new SqlParameter();
                prmActionID.ParameterName = "@ActionID";
                prmActionID.SqlDbType = SqlDbType.Int;
                prmActionID.Value = ActionID;
                command.Parameters.Add(prmActionID);

                prmRoleID = new SqlParameter();
                prmRoleID.ParameterName = "@RoleID";
                prmRoleID.SqlDbType = SqlDbType.Int;
                prmRoleID.Value = RoleID;
                command.Parameters.Add(prmRoleID);

                prmSchoolID = new SqlParameter();
                prmSchoolID.ParameterName = "@SchoolID";
                prmSchoolID.SqlDbType = SqlDbType.Int;
                prmSchoolID.Value = schoolID;
                command.Parameters.Add(prmSchoolID);

                command.Connection.Open();
                int count = 0;
                count = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();
                conn.Dispose();
                return count;
            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public Int32 Insert(RoleInAction objRoleInActionBE)

        {
            SqlConnection conn = null;
            String sqlRoleInActionInsert;
            SqlCommand cmdRoleInActionInsert;
            SqlParameter prmroleInActionID;
            SqlParameter prmactionID;
            SqlParameter prmroleID;
            SqlParameter prmorderNumber;
            SqlParameter prmschoolID;
            SqlParameter prmactive;
            Int32 introleInActionID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());

                sqlRoleInActionInsert = "InsertRoleInAction";

                cmdRoleInActionInsert = new SqlCommand(sqlRoleInActionInsert, conn);
                cmdRoleInActionInsert.CommandType = CommandType.StoredProcedure;

                prmroleInActionID = new SqlParameter();
                prmroleInActionID.Direction = ParameterDirection.ReturnValue;
                prmroleInActionID.SqlDbType = SqlDbType.Int;
                cmdRoleInActionInsert.Parameters.Add(prmroleInActionID);

                prmactionID = new SqlParameter();
                prmactionID.ParameterName = "@actionID";
                prmactionID.SqlDbType = SqlDbType.Int;
                prmactionID.Value = objRoleInActionBE.actionID;
                cmdRoleInActionInsert.Parameters.Add(prmactionID);

                prmroleID = new SqlParameter();
                prmroleID.ParameterName = "@roleID";
                prmroleID.SqlDbType = SqlDbType.Int;
                prmroleID.Value = objRoleInActionBE.roleID;
                cmdRoleInActionInsert.Parameters.Add(prmroleID);

                prmorderNumber = new SqlParameter();
                prmorderNumber.ParameterName = "@orderNumber";
                prmorderNumber.SqlDbType = SqlDbType.Int;
                prmorderNumber.Value = objRoleInActionBE.orderNumber;
                cmdRoleInActionInsert.Parameters.Add(prmorderNumber);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = objRoleInActionBE.schoolID;
                cmdRoleInActionInsert.Parameters.Add(prmschoolID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = objRoleInActionBE.active;
                cmdRoleInActionInsert.Parameters.Add(prmactive);

                cmdRoleInActionInsert.Connection.Open();
                cmdRoleInActionInsert.ExecuteNonQuery();

                introleInActionID = Convert.ToInt32(prmroleInActionID.Value);

                cmdRoleInActionInsert.Connection.Close();
                conn.Dispose();

                return introleInActionID;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

        public void UpdateByActionIDByRoleID(Int32 ActionID_Filter, Int32 RoleID_Filter, Int32 orderNumber, Boolean active, int schoolID)
        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUActionID_Filter;
            SqlParameter prmURoleID_Filter;
            SqlParameter prmUorderNumber;
            SqlParameter prmUactive;
            SqlParameter prmschoolID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateRoleInActionByActionIDByRoleID";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUActionID_Filter = new SqlParameter();
                prmUActionID_Filter.ParameterName = "@ActionID_Filter";
                prmUActionID_Filter.SqlDbType = SqlDbType.Int;
                prmUActionID_Filter.Value = ActionID_Filter;
                command.Parameters.Add(prmUActionID_Filter);

                prmURoleID_Filter = new SqlParameter();
                prmURoleID_Filter.ParameterName = "@RoleID_Filter";
                prmURoleID_Filter.SqlDbType = SqlDbType.Int;
                prmURoleID_Filter.Value = RoleID_Filter;
                command.Parameters.Add(prmURoleID_Filter);

                prmUorderNumber = new SqlParameter();
                prmUorderNumber.ParameterName = "@orderNumber";
                prmUorderNumber.SqlDbType = SqlDbType.Int;
                prmUorderNumber.Value = orderNumber;
                command.Parameters.Add(prmUorderNumber);

                prmUactive = new SqlParameter();
                prmUactive.ParameterName = "@active";
                prmUactive.SqlDbType = SqlDbType.Bit;
                prmUactive.Value = active;
                command.Parameters.Add(prmUactive);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

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


    }
}
