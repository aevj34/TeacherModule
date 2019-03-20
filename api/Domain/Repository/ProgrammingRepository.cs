
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
    public interface ProgrammingRepository
  {

        List<ProgrammingListDto> getCurrentCoursesByTeacherID(Int32 schoolID, Int32 teacherID, Boolean active, Boolean isCurrentPeriod);
        ProgrammingDto Obtain(Int32 programmingID);
        ProgrammingDto ObtainPDF(Int32 programmingID);
        List<ProgrammingListDto1> getCurrentCoursesByTeacherIDToAssistance(Int32 schoolID, Int32 teacherID, Boolean active, Boolean isCurrentPeriod);
        List<ProgrammingListDto> getCurrentCoursesByTutorID(Int32 schoolID, Int32 tutorID, Boolean active, Boolean isCurrentPeriod);

        }
 }

 
