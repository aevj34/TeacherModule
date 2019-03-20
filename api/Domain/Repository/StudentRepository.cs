
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface StudentRepository
 {

  StudentListDto GetBystudentIDByactive(Int32 studentID ,Boolean active  ) ; 

 }
 }

 
