
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface AssistanceDetailRepository
 {

        List<AssistanceDetailDto> GetByprogrammingIDByschoolIDByactive(Int32 programmingID, Int32 schoolID, Boolean active);
        List<AssistanceDetailDto> GetAssistanceDetailByStudent(Int32 enrollmentID, Int32 schoolID, Boolean active);
        void Update(AssistanceDetailDto assistanceDetail);
        Int32 Insert(AssistanceDetailDto assistanceDetail);
    }
 }

 
