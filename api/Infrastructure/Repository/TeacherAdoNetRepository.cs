using api.Application.Dto;
using api.Common.Infrastructure.Security;
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
    public class TeacherAdoNetRepository : TeacherRepository
    {

    public Teacher GetByDni(String Dni)
    {
      SqlConnection conn = null;
      SqlDataReader drUsers;
      String strSqlUser;
      SqlCommand cmdUser;
      SqlParameter prmDni = null;

      try
      {

       Teacher objUser = new Teacher(); ;

        conn = new SqlConnection(Functions.GetConnectionString());

        strSqlUser = "GetTeacherByDni";

        cmdUser = new SqlCommand(strSqlUser, conn);
        cmdUser.CommandType = CommandType.StoredProcedure;

        prmDni = new SqlParameter();
        prmDni.ParameterName = "@Dni";
        prmDni.SqlDbType = SqlDbType.VarChar;
        prmDni.Value = Dni;
        cmdUser.Parameters.Add(prmDni);

        cmdUser.Connection.Open();
        drUsers = cmdUser.ExecuteReader();

        if (drUsers.Read())
        {
          objUser = new Teacher();
          objUser.teacherID = drUsers.GetInt32(drUsers.GetOrdinal("TeacherID"));
          objUser.name = drUsers.GetString(drUsers.GetOrdinal("Name"));
          objUser.shortName = drUsers.GetString(drUsers.GetOrdinal("shortName"));
          objUser.roleID = drUsers.GetInt32(drUsers.GetOrdinal("RoleID"));
          objUser.schoolID = drUsers.GetInt32(drUsers.GetOrdinal("SchoolID"));
          objUser.Dni = drUsers.GetString(drUsers.GetOrdinal("Dni"));
          objUser.Password = drUsers.GetString(drUsers.GetOrdinal("Password"));
        }

        cmdUser.Connection.Close();
        conn.Dispose();

        return objUser;

      }
      catch (Exception ex)
      {
        conn.Dispose();
        throw ex;
      }

    }


    public TeacherDto GetByTeacherID(int TeacherID)
        {

            SqlConnection conn = null;
            SqlDataReader drTeachers;
            String strSqlTeacher;
            SqlCommand cmdTeacher;
            SqlParameter prmTeacherID = null;

            try
            {
                TeacherDto objTeacherBe;
                objTeacherBe = new TeacherDto();
                conn = new SqlConnection(Functions.GetConnectionString());

                strSqlTeacher = "GetTeacherByTeacherID";

                cmdTeacher = new SqlCommand(strSqlTeacher, conn);
                cmdTeacher.CommandType = CommandType.StoredProcedure;

                prmTeacherID = new SqlParameter();
                prmTeacherID.ParameterName = "@TeacherID";
                prmTeacherID.SqlDbType = SqlDbType.Int;
                prmTeacherID.Value = TeacherID;
                cmdTeacher.Parameters.Add(prmTeacherID);

                cmdTeacher.Connection.Open();
                drTeachers = cmdTeacher.ExecuteReader();

                AmazonS3 s3 = new AmazonS3();
                s3 = AmazonS3Factory.setAmazonS3();

                if (drTeachers.Read())
                {
                    //Instancio el objeto y le agrego cada uno de sus campos
                    objTeacherBe = new TeacherDto();
                    objTeacherBe.TeacherID = drTeachers.GetInt32(drTeachers.GetOrdinal("TeacherID"));
                    objTeacherBe.Name = drTeachers.GetString(drTeachers.GetOrdinal("Name"));
                    objTeacherBe.ShortName = drTeachers.GetString(drTeachers.GetOrdinal("ShortName"));

                    objTeacherBe.Gender = drTeachers.GetString(drTeachers.GetOrdinal("Gender"));
                    objTeacherBe.SchoolID = drTeachers.GetInt32(drTeachers.GetOrdinal("SchoolID"));
                    objTeacherBe.Active = drTeachers.GetBoolean(drTeachers.GetOrdinal("Active"));
                    objTeacherBe.Role_Name = drTeachers.GetString(drTeachers.GetOrdinal("Role_Name"));

                    objTeacherBe.Email = drTeachers.GetString(drTeachers.GetOrdinal("Email"));
                    objTeacherBe.AlternativeMail = drTeachers.GetString(drTeachers.GetOrdinal("AlternativeMail"));
                    objTeacherBe.HomeAddress = drTeachers.GetString(drTeachers.GetOrdinal("HomeAddress"));
                    objTeacherBe.Phone = drTeachers.GetString(drTeachers.GetOrdinal("Phone"));

                    objTeacherBe.LastLoginDate = drTeachers.GetDateTime(drTeachers.GetOrdinal("LastLoginDate"));
                    objTeacherBe.LastPasswordChangedDate = drTeachers.GetDateTime(drTeachers.GetOrdinal("LastPasswordChangedDate"));

                    objTeacherBe.LastLoginDateShow = TimerAgo.TimeShow(objTeacherBe.LastLoginDate, Formater.ShortDateTime());
                    objTeacherBe.LastPasswordChangedDateShow = TimerAgo.TimeShow(objTeacherBe.LastPasswordChangedDate, Formater.ShortDateTime());
                    objTeacherBe.LastLogoutDateShow = TimerAgo.TimeShow(objTeacherBe.LastLogoutDate, Formater.ShortDateTime());

                    objTeacherBe.LastLoginDateAgo = TimerAgo.TimeAgo(objTeacherBe.LastLoginDate);
                    objTeacherBe.LastPasswordChangedDateAgo = TimerAgo.TimeAgo(objTeacherBe.LastPasswordChangedDate);
                    objTeacherBe.LastLogoutDateAgo = TimerAgo.TimeAgo(objTeacherBe.LastLogoutDate);

                    objTeacherBe.School_Name = drTeachers.GetString(drTeachers.GetOrdinal("School_Name"));
                    objTeacherBe.ImageKey = drTeachers.GetString(drTeachers.GetOrdinal("ImageKey"));
                    objTeacherBe.EndPoint = s3.getTeacherEndPoint(objTeacherBe.SchoolID, objTeacherBe.ImageKey);

                }

                cmdTeacher.Connection.Close();
                conn.Dispose();

                return objTeacherBe;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }

    
    }
}
