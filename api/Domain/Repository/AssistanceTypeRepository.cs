
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface AssistanceTypeRepository
 {

        List<AssistanceTypeListDto> GetByschoolIDByactive(Int32 schoolID, Boolean active);
    }
 }

 
