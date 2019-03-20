
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
using api.Domain.Entity;

namespace api.Domain.Repository
 {
 public interface AssistanceRepository
 {

        List<AssistanceListDto> GetLessons(Int32 schoolID, Int32 programmingID, Boolean active);
        Int32 Insert(Assistance assistance);
        void Update(Assistance assistanceDto);
        void UpdateByassistanceID(Int32 assistanceID_Filter, Boolean active);

    }
}

 
