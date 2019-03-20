
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
 namespace api.Infrastructure.Repository
 {
 public class EvaluationAdoNet:EvaluationRepository
 {

  public List < EvaluationListDto> GetEvaluations( Int32 evaluationFormulaID ,Int32 schoolID ,Boolean active, int teacherTypeID) 
  { 

   SqlConnection conn = null ; 
   SqlDataReader reader;
   String sql ;
   SqlCommand command;
   SqlParameter prmevaluationFormulaID  = null ;  
   SqlParameter prmschoolID  = null ;  
   SqlParameter prmactive  = null ;
   SqlParameter prmteacherTypeID = null;

   try 
   {
	 EvaluationListDto evaluation ;
	 List < EvaluationListDto > lstEvaluations ;

	 conn = new SqlConnection (Functions.GetConnectionString()) ;

	 sql = "GetEvaluationByevaluationFormulaIDByschoolIDByactive";

	 command = new SqlCommand ( sql, conn );
	 command.CommandType = CommandType .StoredProcedure;

	 prmevaluationFormulaID = new SqlParameter()  ; 
	 prmevaluationFormulaID.ParameterName = "@evaluationFormulaID" ; 
	 prmevaluationFormulaID.SqlDbType = SqlDbType.Int ; 
	 prmevaluationFormulaID.Value = evaluationFormulaID ; 
	 command.Parameters.Add( prmevaluationFormulaID ) ; 

	 prmschoolID = new SqlParameter()  ; 
	 prmschoolID.ParameterName = "@schoolID" ; 
	 prmschoolID.SqlDbType = SqlDbType.Int ; 
	 prmschoolID.Value = schoolID ; 
	 command.Parameters.Add( prmschoolID ) ; 

	 prmactive = new SqlParameter()  ; 
	 prmactive.ParameterName = "@active" ; 
	 prmactive.SqlDbType = SqlDbType.Bit ; 
	 prmactive.Value = active ; 
	 command.Parameters.Add( prmactive ) ;

     prmteacherTypeID = new SqlParameter();
     prmteacherTypeID.ParameterName = "@teacherTypeID";
     prmteacherTypeID.SqlDbType = SqlDbType.Int;
     prmteacherTypeID.Value = teacherTypeID;
     command.Parameters.Add(prmteacherTypeID);

    command.Connection.Open();
	 reader = command.ExecuteReader();

	 lstEvaluations = new List< EvaluationListDto >();

	 while (reader.Read())
	 {
		 evaluation = new EvaluationListDto ();
		 evaluation.evaluationID = reader.GetInt32(reader.GetOrdinal("evaluationID")) ; 
		 evaluation.name = reader.GetString(reader.GetOrdinal("name")) ; 
		 evaluation.weight = reader.GetDecimal(reader.GetOrdinal("weight")) ; 
		 evaluation.isAverage = reader.GetBoolean(reader.GetOrdinal("isAverage")) ; 
		 evaluation.evaluationType_abbreviation = reader.GetString(reader.GetOrdinal("evaluationType_abbreviation")) ;
         evaluation.evaluationType_name = reader.GetString(reader.GetOrdinal("evaluationType_name"));
         lstEvaluations.Add(evaluation);

	 } 

	 command.Connection.Close();
	 conn.Dispose();

	 return lstEvaluations;

   } 
   catch ( Exception ex ) 
    {
	 conn.Dispose();
	 throw ex;
    }

  }
        public List<EvaluationListDto> GetEvaluationsLegend(Int32 evaluationFormulaID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmevaluationFormulaID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                EvaluationListDto evaluation;
                List<EvaluationListDto> lstEvaluations;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetEvaluationLegendByevaluationFormulaIDByschoolIDByactive";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmevaluationFormulaID = new SqlParameter();
                prmevaluationFormulaID.ParameterName = "@evaluationFormulaID";
                prmevaluationFormulaID.SqlDbType = SqlDbType.Int;
                prmevaluationFormulaID.Value = evaluationFormulaID;
                command.Parameters.Add(prmevaluationFormulaID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = schoolID;
                command.Parameters.Add(prmschoolID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = active;
                command.Parameters.Add(prmactive);

                command.Connection.Open();
                reader = command.ExecuteReader();

                lstEvaluations = new List<EvaluationListDto>();

                while (reader.Read())
                {
                    evaluation = new EvaluationListDto();
                    evaluation.isAverage = reader.GetBoolean(reader.GetOrdinal("isAverage"));
                    evaluation.evaluationType_abbreviation = reader.GetString(reader.GetOrdinal("evaluationType_abbreviation"));
                    evaluation.evaluationType_name = reader.GetString(reader.GetOrdinal("evaluationType_name"));
                    lstEvaluations.Add(evaluation);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstEvaluations;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }
    }
 }

 
