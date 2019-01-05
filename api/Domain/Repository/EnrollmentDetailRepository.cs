
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface EnrollmentDetailRepository
 {

        List<EnrollmentDetailListDto> GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active);
    }
 }

 
