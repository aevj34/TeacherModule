
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface EvaluationExpirationRepository
 {
        List<EvaluationExpirationListDto> GetByprogrammingIDByactive(Int32 programmingID, bool active);
    }
 }

 
