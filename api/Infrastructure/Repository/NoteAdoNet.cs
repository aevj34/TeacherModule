
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

        public List<TutorNoteDto> GetNotesByStudent(Int32 enrollmentID, Int32 studentID, Int32 schoolID, Boolean active)
        {

            SqlConnection conn = null;
            SqlDataReader reader;
            String sql;
            SqlCommand command;
            SqlParameter prmenrollmentID = null;
            SqlParameter prmstudentID = null;
            SqlParameter prmschoolID = null;
            SqlParameter prmactive = null;

            try
            {
                TutorNoteDto note;
                List<TutorNoteDto> lstNotes;

                conn = new SqlConnection(Functions.GetConnectionString());

                sql = "GetNoteByStudent";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmenrollmentID = new SqlParameter();
                prmenrollmentID.ParameterName = "@enrollmentID";
                prmenrollmentID.SqlDbType = SqlDbType.Int;
                prmenrollmentID.Value = enrollmentID;
                command.Parameters.Add(prmenrollmentID);

                prmstudentID = new SqlParameter();
                prmstudentID.ParameterName = "@studentID";
                prmstudentID.SqlDbType = SqlDbType.Int;
                prmstudentID.Value = studentID;
                command.Parameters.Add(prmstudentID);

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

                lstNotes = new List<TutorNoteDto>();

                while (reader.Read())
                {
                    note = new TutorNoteDto();
                    note.noteID = reader.GetInt32(reader.GetOrdinal("noteID"));
                    if (reader.GetDecimal(reader.GetOrdinal("note")) == 100)
                        note.note = "";
                    else
                        note.note = reader.GetDecimal(reader.GetOrdinal("note")).ToString();
                    note.courseID = reader.GetInt32(reader.GetOrdinal("courseID"));
                    note.schoolYearID = reader.GetInt32(reader.GetOrdinal("schoolYearID"));
                    note.evaluationID = reader.GetInt32(reader.GetOrdinal("evaluationID"));
                    lstNotes.Add(note);

                }

                command.Connection.Close();
                conn.Dispose();

                return lstNotes;

            }
            catch (Exception ex)
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
            SqlParameter prmenrollmentDetailID;
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
            SqlParameter prmgrade;
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

                prmenrollmentDetailID = new SqlParameter();
                prmenrollmentDetailID.ParameterName = "@enrollmentDetailID";
                prmenrollmentDetailID.SqlDbType = SqlDbType.Int;
                prmenrollmentDetailID.Value = note.enrollmentDetailID;
                cmdNoteInsert.Parameters.Add(prmenrollmentDetailID);

                if (note.note != "100")
                {
                    prmnote = new SqlParameter();
                    prmnote.ParameterName = "@note";
                    prmnote.SqlDbType = SqlDbType.Decimal;
                    prmnote.Value = note.note;
                    cmdNoteInsert.Parameters.Add(prmnote);
                }
                
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

                prmgrade = new SqlParameter();
                prmgrade.ParameterName = "@grade";
                prmgrade.SqlDbType = SqlDbType.Int;
                prmgrade.Value = note.grade;
                cmdNoteInsert.Parameters.Add(prmgrade);

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


        public void Update(Note noteDto)

        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUnoteID;
            SqlParameter prmUenrollmentID;
            SqlParameter prmUenrollmentDetailID;
            SqlParameter prmUnote;
            SqlParameter prmUstudentID;
            SqlParameter prmUcourseID;
            SqlParameter prmUschoolID;
            SqlParameter prmUheadquartersID;
            SqlParameter prmUcareerID;
            SqlParameter prmUschoolYearID;
            SqlParameter prmUprogrammingID;
            SqlParameter prmUevaluationPeriodID;
            SqlParameter prmUturnID;
            SqlParameter prmUsectionID;
            SqlParameter prmUperiodTypeID;
            SqlParameter prmUevaluationID;
            SqlParameter prmUgrade;
            SqlParameter prmUactive;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateNote";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUnoteID = new SqlParameter();
                prmUnoteID.ParameterName = "@noteID";
                prmUnoteID.SqlDbType = SqlDbType.Int;
                prmUnoteID.Value = noteDto.noteID;
                command.Parameters.Add(prmUnoteID);

                prmUenrollmentID = new SqlParameter();
                prmUenrollmentID.ParameterName = "@enrollmentID";
                prmUenrollmentID.SqlDbType = SqlDbType.Int;
                prmUenrollmentID.Value = noteDto.enrollmentID;
                command.Parameters.Add(prmUenrollmentID);

                prmUenrollmentDetailID = new SqlParameter();
                prmUenrollmentDetailID.ParameterName = "@enrollmentDetailID";
                prmUenrollmentDetailID.SqlDbType = SqlDbType.Int;
                prmUenrollmentDetailID.Value = noteDto.enrollmentDetailID;
                command.Parameters.Add(prmUenrollmentDetailID);

                prmUnote = new SqlParameter();
                prmUnote.ParameterName = "@note";
                prmUnote.SqlDbType = SqlDbType.Decimal;
                prmUnote.Value = noteDto.note;
                command.Parameters.Add(prmUnote);

                prmUstudentID = new SqlParameter();
                prmUstudentID.ParameterName = "@studentID";
                prmUstudentID.SqlDbType = SqlDbType.Int;
                prmUstudentID.Value = noteDto.studentID;
                command.Parameters.Add(prmUstudentID);

                prmUcourseID = new SqlParameter();
                prmUcourseID.ParameterName = "@courseID";
                prmUcourseID.SqlDbType = SqlDbType.Int;
                prmUcourseID.Value = noteDto.courseID;
                command.Parameters.Add(prmUcourseID);

                prmUschoolID = new SqlParameter();
                prmUschoolID.ParameterName = "@schoolID";
                prmUschoolID.SqlDbType = SqlDbType.Int;
                prmUschoolID.Value = noteDto.schoolID;
                command.Parameters.Add(prmUschoolID);

                prmUheadquartersID = new SqlParameter();
                prmUheadquartersID.ParameterName = "@headquartersID";
                prmUheadquartersID.SqlDbType = SqlDbType.Int;
                prmUheadquartersID.Value = noteDto.headquartersID;
                command.Parameters.Add(prmUheadquartersID);

                prmUcareerID = new SqlParameter();
                prmUcareerID.ParameterName = "@careerID";
                prmUcareerID.SqlDbType = SqlDbType.Int;
                prmUcareerID.Value = noteDto.careerID;
                command.Parameters.Add(prmUcareerID);

                prmUschoolYearID = new SqlParameter();
                prmUschoolYearID.ParameterName = "@schoolYearID";
                prmUschoolYearID.SqlDbType = SqlDbType.Int;
                prmUschoolYearID.Value = noteDto.schoolYearID;
                command.Parameters.Add(prmUschoolYearID);

                prmUprogrammingID = new SqlParameter();
                prmUprogrammingID.ParameterName = "@programmingID";
                prmUprogrammingID.SqlDbType = SqlDbType.Int;
                prmUprogrammingID.Value = noteDto.programmingID;
                command.Parameters.Add(prmUprogrammingID);

                prmUevaluationPeriodID = new SqlParameter();
                prmUevaluationPeriodID.ParameterName = "@evaluationPeriodID";
                prmUevaluationPeriodID.SqlDbType = SqlDbType.Int;
                prmUevaluationPeriodID.Value = noteDto.evaluationPeriodID;
                command.Parameters.Add(prmUevaluationPeriodID);

                prmUturnID = new SqlParameter();
                prmUturnID.ParameterName = "@turnID";
                prmUturnID.SqlDbType = SqlDbType.Int;
                prmUturnID.Value = noteDto.turnID;
                command.Parameters.Add(prmUturnID);

                prmUsectionID = new SqlParameter();
                prmUsectionID.ParameterName = "@sectionID";
                prmUsectionID.SqlDbType = SqlDbType.Int;
                prmUsectionID.Value = noteDto.sectionID;
                command.Parameters.Add(prmUsectionID);

                prmUperiodTypeID = new SqlParameter();
                prmUperiodTypeID.ParameterName = "@periodTypeID";
                prmUperiodTypeID.SqlDbType = SqlDbType.Int;
                prmUperiodTypeID.Value = noteDto.periodTypeID;
                command.Parameters.Add(prmUperiodTypeID);

                prmUevaluationID = new SqlParameter();
                prmUevaluationID.ParameterName = "@evaluationID";
                prmUevaluationID.SqlDbType = SqlDbType.Int;
                prmUevaluationID.Value = noteDto.evaluationID;
                command.Parameters.Add(prmUevaluationID);

                prmUgrade = new SqlParameter();
                prmUgrade.ParameterName = "@grade";
                prmUgrade.SqlDbType = SqlDbType.Int;
                prmUgrade.Value = noteDto.grade;
                command.Parameters.Add(prmUgrade);

                prmUactive = new SqlParameter();
                prmUactive.ParameterName = "@active";
                prmUactive.SqlDbType = SqlDbType.Bit;
                prmUactive.Value = noteDto.active;
                command.Parameters.Add(prmUactive);

                command.Connection.Open();
                command.ExecuteNonQuery();

                command.Connection.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }


        public void UpdateWhiteNoteBynoteID(Int32 noteID_Filter)
        {
            SqlConnection conn = null;
            String sql;
            SqlCommand command;
            SqlParameter prmUnoteID_Filter;
            SqlParameter prmUnote;

            try
            {
                conn = new SqlConnection(Functions.GetConnectionString());
                sql = "UpdateNoteBynoteID";
                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.StoredProcedure;

                prmUnoteID_Filter = new SqlParameter();
                prmUnoteID_Filter.ParameterName = "@noteID_Filter";
                prmUnoteID_Filter.SqlDbType = SqlDbType.Int;
                prmUnoteID_Filter.Value = noteID_Filter;
                command.Parameters.Add(prmUnoteID_Filter);

                prmUnote = new SqlParameter();
                prmUnote.ParameterName = "@note";
                prmUnote.SqlDbType = SqlDbType.Decimal;
                prmUnote.Value = null;
                command.Parameters.Add(prmUnote);

                command.Connection.Open();
                command.ExecuteNonQuery();

                command.Connection.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }

        }



    }
}

 
