using api.Domain.Repository;
using System;

namespace api.Domain.Entity
  {
    public class Teacher
    {
	public Int32 teacherID { get; set; }
	public String name { get; set; }
	public String otherName { get; set; }
	public String firstName { get; set; }
	public String lastName { get; set; }
	public String Gender { get; set; }
	public String Dni { get; set; }
	public Int32 schoolID { get; set; }
	public DateTime dateofBirth { get; set; }
	public Int32 roleID { get; set; }
	public String password { get; set; }
	public Boolean canLogin { get; set; }
	public Boolean forceResetPassword { get; set; }
	public String email { get; set; }
	public String alternativeMail { get; set; }
	public String homeAddress { get; set; }
	public String phone { get; set; }
	public DateTime lastLoginDate { get; set; }
	public DateTime lastPasswordChangedDate { get; set; }
	public DateTime lastLogoutDate { get; set; }
	public String ImageKey { get; set; }
	public DateTime startDateSchool { get; set; }
	public String lastSchool { get; set; }
	public Boolean active { get; set; }
        public String endPoint { get; set; }
        public String shortName { get; set; }

    }
  }

 
