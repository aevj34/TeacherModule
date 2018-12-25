using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class TeacherDto
    {
        public TeacherDto()
        {
        }

    public Int32 TeacherID { get; set; }

    public String Name { get; set; }

    public String OtherName { get; set; }

    public String FirstName { get; set; }

    public String LastName { get; set; }

    public String Gender { get; set; }

    public DateTime DateofBirth { get; set; }

    public Boolean Active { get; set; }

    public Int32 RoleID { get; set; }

    public String Dni { get; set; }

    public String Password { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Boolean CanLogin { get; set; }

    public Boolean ForceResetPassword { get; set; }

    public String Email { get; set; }

    public String AlternativeMail { get; set; }

    public String HomeAddress { get; set; }

    public int CountryID { get; set; }

    public String Phone { get; set; }

    public String Website { get; set; }

    public String LastSchool { get; set; }

    public String StartDateSchool { get; set; }

    public DateTime LastLoginDate { get; set; }

    public DateTime LastPasswordChangedDate { get; set; }

    public DateTime LastLogoutDate { get; set; }

    public Int32 SchoolID { get; set; }

    public String Role_Name { get; set; }

    public Int32 Nro { get; set; }

    public string DateofBirthString { get; set; }

    public String ShortName { get; set; }

    public String School_Name { get; set; }

    public String DateofBirthShow { get; set; }
    public String LastLoginDateShow { get; set; }
    public String LastPasswordChangedDateShow { get; set; }
    public  String LastLogoutDateShow { get; set; }

    public String LastLoginDateAgo { get; set; }
    public String LastPasswordChangedDateAgo { get; set; }
    public String LastLogoutDateAgo { get; set; }

    public String EndPoint { get; set; }
    public String ImageKey { get; set; }

  }
}
