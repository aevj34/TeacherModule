
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface EvaluationDetailRepository
 {

  List < EvaluationDetailListDto> GetEvaluationsByEvaluationAverageID(Int32 evaluationAverageID  ) ; 
 }
 }

 
