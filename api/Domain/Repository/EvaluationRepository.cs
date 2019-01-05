
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface EvaluationRepository
 {

  List < EvaluationListDto> GetEvaluations(Int32 evaluationFormulaID ,Int32 schoolID ,Boolean active  ) ; 
 }
 }

 
