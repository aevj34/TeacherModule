
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 using api.Domain.Repository;
 namespace api.Infrastructure.Repository
 {
 public class NoteChangeAdoNet:NoteChangeRepository
 {


 public Int32 Insert( NoteChangeDto  noteChange )

 {
   SqlConnection conn  = null ; 
   String sqlNoteChangeInsert;
   SqlCommand cmdNoteChangeInsert;
   SqlParameter prmnoteChangeID; 
   SqlParameter prmnoteID; 
   SqlParameter prmnote; 
   SqlParameter prmteacherID; 
   SqlParameter prmuserID; 
   SqlParameter prmchangeDate; 
   SqlParameter prmschoolID; 
   Int32 intnoteChangeID;

   try 
   {
	 conn = new SqlConnection(Functions.GetConnectionString());

	 sqlNoteChangeInsert = "InsertNoteChange";

	 cmdNoteChangeInsert = new SqlCommand(sqlNoteChangeInsert,conn);
	 cmdNoteChangeInsert.CommandType = CommandType.StoredProcedure;

	 prmnoteChangeID = new SqlParameter () ;
	 prmnoteChangeID.Direction = ParameterDirection.ReturnValue;
	 prmnoteChangeID.SqlDbType = SqlDbType .Int;
	 cmdNoteChangeInsert.Parameters.Add(prmnoteChangeID);

	 prmnoteID = new SqlParameter ();
	 prmnoteID.ParameterName = "@noteID";
	 prmnoteID.SqlDbType = SqlDbType.Int;
	 prmnoteID.Value = noteChange.noteID;
	 cmdNoteChangeInsert.Parameters.Add(prmnoteID);

      if (noteChange.note != "100")
      {
        prmnote = new SqlParameter();
        prmnote.ParameterName = "@note";
        prmnote.SqlDbType = SqlDbType.Decimal;
        prmnote.Value = noteChange.note;
        cmdNoteChangeInsert.Parameters.Add(prmnote);
      }

      prmteacherID = new SqlParameter ();
	 prmteacherID.ParameterName = "@teacherID";
	 prmteacherID.SqlDbType = SqlDbType.Int;
	 prmteacherID.Value = noteChange.teacherID;
	 cmdNoteChangeInsert.Parameters.Add(prmteacherID);

	 prmuserID = new SqlParameter ();
	 prmuserID.ParameterName = "@userID";
	 prmuserID.SqlDbType = SqlDbType.Int;
	 prmuserID.Value = noteChange.userID;
	 cmdNoteChangeInsert.Parameters.Add(prmuserID);

	 prmchangeDate = new SqlParameter ();
	 prmchangeDate.ParameterName = "@changeDate";
	 prmchangeDate.SqlDbType = SqlDbType.DateTime;
	 prmchangeDate.Value = noteChange.changeDate;
	 cmdNoteChangeInsert.Parameters.Add(prmchangeDate);

	 prmschoolID = new SqlParameter ();
	 prmschoolID.ParameterName = "@schoolID";
	 prmschoolID.SqlDbType = SqlDbType.Int;
	 prmschoolID.Value = noteChange.schoolID;
	 cmdNoteChangeInsert.Parameters.Add(prmschoolID);

	 cmdNoteChangeInsert.Connection.Open();
	 cmdNoteChangeInsert.ExecuteNonQuery();

	 intnoteChangeID = Convert.ToInt32(prmnoteChangeID.Value);

	 cmdNoteChangeInsert.Connection.Close();
	 conn.Dispose();

	 return  intnoteChangeID;

 }
   catch ( Exception ex ) 
    {
	 conn.Dispose();
	 throw ex;
    }

  } 

 }
 }

 
