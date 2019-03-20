
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
        SchoolYearListDto Obtain(Int32 schoolYearID);
    }
 }

 
