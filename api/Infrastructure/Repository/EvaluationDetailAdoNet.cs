
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
 namespace api.Infrastructure.Repository
 {
 public class EvaluationDetailAdoNet:EvaluationDetailRepository
 {

  public List < EvaluationDetailListDto> GetEvaluationsByEvaluationAverageID( Int32 evaluationAverageID  ) 
  { 

   SqlConnection conn = null ; 
   SqlDataReader reader;
   String sql ;
   SqlCommand command;
   SqlParameter prmevaluationAverageID  = null ;  

   try 
   {
	 EvaluationDetailListDto evaluationDetail ;
	 List < EvaluationDetailListDto > lstEvaluationDetails ;

	 conn = new SqlConnection (Functions.GetConnectionString()) ;

	 sql = "GetEvaluationDetailByevaluationAverageID";

	 command = new SqlCommand ( sql, conn );
	 command.CommandType = CommandType .StoredProcedure;

	 prmevaluationAverageID = new SqlParameter()  ; 
	 prmevaluationAverageID.ParameterName = "@evaluationAverageID" ; 
	 prmevaluationAverageID.SqlDbType = SqlDbType.Int ; 
	 prmevaluationAverageID.Value = evaluationAverageID ; 
	 command.Parameters.Add( prmevaluationAverageID ) ; 

	 command.Connection.Open();
	 reader = command.ExecuteReader();

	 lstEvaluationDetails = new List< EvaluationDetailListDto >();

	 while (reader.Read())
	 {
		 evaluationDetail = new EvaluationDetailListDto ();
		 evaluationDetail.evaluationDetailID = reader.GetInt32(reader.GetOrdinal("evaluationDetailID")) ; 
		 evaluationDetail.evaluationID = reader.GetInt32(reader.GetOrdinal("evaluationID")) ; 
		 evaluationDetail.evaluationAverageID = reader.GetInt32(reader.GetOrdinal("evaluationAverageID")) ;
         evaluationDetail.evaluation_weight = reader.GetDecimal(reader.GetOrdinal("evaluation_weight"));
         lstEvaluationDetails.Add(evaluationDetail);

	 } 

	 command.Connection.Close();
	 conn.Dispose();

	 return lstEvaluationDetails;

   } 
   catch ( Exception ex ) 
    {
	 conn.Dispose();
	 throw ex;
    }

  } 


 }
 }

 
