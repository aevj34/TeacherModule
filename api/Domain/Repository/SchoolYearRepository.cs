
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
using api.Domain.Entity;

namespace api.Domain.Repository
 {
 public interface SchoolYearRepository
 {
        List<SchoolYearListDto> GetBycurrentYearByschoolIDByactive(Boolean currentYear, Int32 schoolID, Boolean active);
    }
 }

 
