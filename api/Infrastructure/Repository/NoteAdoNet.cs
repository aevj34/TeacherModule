
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
using api.Domain.Entity;

namespace api.Infrastructure.Repository
 {
 public class NoteAdoNet:NoteRepository
 {

  public List < NoteListDto> GetByprogrammingIDByschoolIDByactive(Int32 programmingID ,Int32 schoolID ,Boolean active  ) 
  { 

   SqlConnection conn = null ; 
   SqlDataReader reader;
   String sql ;
   SqlCommand command;
   SqlParameter prmprogrammingID  = null ;  
   SqlParameter prmschoolID  = null ;  
   SqlParameter prmactive  = null ;  

   try 
   {
	 NoteListDto note ;
	 List < NoteListDto > lstNotes ;

	 conn = new SqlConnection (Functions.GetConnectionString()) ;

	 sql = "GetNoteByprogrammingIDByschoolIDByactive";

	 command = new SqlCommand ( sql, conn );
	 command.CommandType = CommandType .StoredProcedure;

	 prmprogrammingID = new SqlParameter()  ; 
	 prmprogrammingID.ParameterName = "@programmingID" ; 
	 prmprogrammingID.SqlDbType = SqlDbType.Int ; 
	 prmprogrammingID.Value = programmingID ; 
	 command.Parameters.Add( prmprogrammingID ) ; 

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

	 command.Connection.Open();
	 reader = command.ExecuteReader();

	 lstNotes = new List< NoteListDto >();

	 while (reader.Read())
	 {
		 note = new NoteListDto ();
         note.noteID = reader.GetInt32(reader.GetOrdinal("noteID"));
          if (reader.GetDecimal(reader.GetOrdinal("note")) == 100)
             note.note = "";
          else
             note.note = reader.GetDecimal(reader.GetOrdinal("note")).ToString();
		 note.studentID = reader.GetInt32(reader.GetOrdinal("studentID")) ; 
		 note.evaluationID = reader.GetInt32(reader.GetOrdinal("evaluationID")) ; 
		 lstNotes.Add(note);

	 } 

	 command.Connection.Close();
	 conn.Dispose();

	 return lstNotes;

   } 
   catch ( Exception ex ) 
    {
	 conn.Dispose();
	 throw ex;
    }

  }



        public Int32 Insert(Note note)

        {
            SqlConnection conn = null;
            String sqlNoteInsert;
            SqlCommand cmdNoteInsert;
            SqlParameter prmnoteID;
            SqlParameter prmenrollmentID;
            SqlParameter prmenrollmentDetail;
            SqlParameter prmnote;
            SqlParameter prmstudentID;
            SqlParameter prmcourseID;
            SqlParameter prmschoolID;
            SqlParameter prmheadquartersID;
            SqlParameter prmcareerID;
            SqlParameter prmschoolYearID;
            SqlParameter prmprogrammingID;
            SqlParameter prmevaluationPeriodID;
            SqlParameter prmturnID;
            SqlParameter prmsectionID;
            SqlParameter prmperiodTypeID;
            SqlParameter prmevaluationID;
            SqlParameter prmactive;
            Int32 intnoteID;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());

                sqlNoteInsert = "InsertNote";

                cmdNoteInsert = new SqlCommand(sqlNoteInsert, conn);
                cmdNoteInsert.CommandType = CommandType.StoredProcedure;

                prmnoteID = new SqlParameter();
                prmnoteID.Direction = ParameterDirection.ReturnValue;
                prmnoteID.SqlDbType = SqlDbType.Int;
                cmdNoteInsert.Parameters.Add(prmnoteID);

                prmenrollmentID = new SqlParameter();
                prmenrollmentID.ParameterName = "@enrollmentID";
                prmenrollmentID.SqlDbType = SqlDbType.Int;
                prmenrollmentID.Value = note.enrollmentID;
                cmdNoteInsert.Parameters.Add(prmenrollmentID);

                prmenrollmentDetail = new SqlParameter();
                prmenrollmentDetail.ParameterName = "@enrollmentDetail";
                prmenrollmentDetail.SqlDbType = SqlDbType.Int;
                prmenrollmentDetail.Value = note.enrollmentDetail;
                cmdNoteInsert.Parameters.Add(prmenrollmentDetail);

                prmnote = new SqlParameter();
                prmnote.ParameterName = "@note";
                prmnote.SqlDbType = SqlDbType.Decimal;
                prmnote.Value = note.note;
                cmdNoteInsert.Parameters.Add(prmnote);

                prmstudentID = new SqlParameter();
                prmstudentID.ParameterName = "@studentID";
                prmstudentID.SqlDbType = SqlDbType.Int;
                prmstudentID.Value = note.studentID;
                cmdNoteInsert.Parameters.Add(prmstudentID);

                prmcourseID = new SqlParameter();
                prmcourseID.ParameterName = "@courseID";
                prmcourseID.SqlDbType = SqlDbType.Int;
                prmcourseID.Value = note.courseID;
                cmdNoteInsert.Parameters.Add(prmcourseID);

                prmschoolID = new SqlParameter();
                prmschoolID.ParameterName = "@schoolID";
                prmschoolID.SqlDbType = SqlDbType.Int;
                prmschoolID.Value = note.schoolID;
                cmdNoteInsert.Parameters.Add(prmschoolID);

                prmheadquartersID = new SqlParameter();
                prmheadquartersID.ParameterName = "@headquartersID";
                prmheadquartersID.SqlDbType = SqlDbType.Int;
                prmheadquartersID.Value = note.headquartersID;
                cmdNoteInsert.Parameters.Add(prmheadquartersID);

                prmcareerID = new SqlParameter();
                prmcareerID.ParameterName = "@careerID";
                prmcareerID.SqlDbType = SqlDbType.Int;
                prmcareerID.Value = note.careerID;
                cmdNoteInsert.Parameters.Add(prmcareerID);

                prmschoolYearID = new SqlParameter();
                prmschoolYearID.ParameterName = "@schoolYearID";
                prmschoolYearID.SqlDbType = SqlDbType.Int;
                prmschoolYearID.Value = note.schoolYearID;
                cmdNoteInsert.Parameters.Add(prmschoolYearID);

                prmprogrammingID = new SqlParameter();
                prmprogrammingID.ParameterName = "@programmingID";
                prmprogrammingID.SqlDbType = SqlDbType.Int;
                prmprogrammingID.Value = note.programmingID;
                cmdNoteInsert.Parameters.Add(prmprogrammingID);

                prmevaluationPeriodID = new SqlParameter();
                prmevaluationPeriodID.ParameterName = "@evaluationPeriodID";
                prmevaluationPeriodID.SqlDbType = SqlDbType.Int;
                prmevaluationPeriodID.Value = note.evaluationPeriodID;
                cmdNoteInsert.Parameters.Add(prmevaluationPeriodID);

                prmturnID = new SqlParameter();
                prmturnID.ParameterName = "@turnID";
                prmturnID.SqlDbType = SqlDbType.Int;
                prmturnID.Value = note.turnID;
                cmdNoteInsert.Parameters.Add(prmturnID);

                prmsectionID = new SqlParameter();
                prmsectionID.ParameterName = "@sectionID";
                prmsectionID.SqlDbType = SqlDbType.Int;
                prmsectionID.Value = note.sectionID;
                cmdNoteInsert.Parameters.Add(prmsectionID);

                prmperiodTypeID = new SqlParameter();
                prmperiodTypeID.ParameterName = "@periodTypeID";
                prmperiodTypeID.SqlDbType = SqlDbType.Int;
                prmperiodTypeID.Value = note.periodTypeID;
                cmdNoteInsert.Parameters.Add(prmperiodTypeID);

                prmevaluationID = new SqlParameter();
                prmevaluationID.ParameterName = "@evaluationID";
                prmevaluationID.SqlDbType = SqlDbType.Int;
                prmevaluationID.Value = note.evaluationID;
                cmdNoteInsert.Parameters.Add(prmevaluationID);

                prmactive = new SqlParameter();
                prmactive.ParameterName = "@active";
                prmactive.SqlDbType = SqlDbType.Bit;
                prmactive.Value = note.active;
                cmdNoteInsert.Parameters.Add(prmactive);

                cmdNoteInsert.Connection.Open();
                cmdNoteInsert.ExecuteNonQuery();

                intnoteID = Convert.ToInt32(prmnoteID.Value);

                cmdNoteInsert.Connection.Close();
                conn.Dispose();

                return intnoteID;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}

 
