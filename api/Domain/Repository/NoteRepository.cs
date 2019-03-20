
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
using api.Domain.Entity;

namespace api.Domain.Repository
{
    public interface NoteRepository
    {

        List<NoteListDto> GetByprogrammingIDByschoolIDByactive(Int32 programmingID, Int32 schoolID, Boolean active);
        List<TutorNoteDto> GetNotesByStudent(Int32 enrollmentID, Int32 studentID, Int32 schoolID, Boolean active);
        Int32 Insert(Note note);
        void Update(Note note);
        void UpdateWhiteNoteBynoteID(Int32 noteID_Filter);

    }
}
 
