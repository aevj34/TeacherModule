
 using api.Application.Dto;
 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Data;
 namespace api.Domain.Repository
 {
 public interface EnrollmentDetailRepository
 {

        List<EnrollmentDetailListDto> GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active);
        List<EnrollmentDetailListDto> GetStudentsByTutorID(Int32 schoolID, int tutorID, bool isCurrentPeriod, Boolean active, int skip, int pageSize);
        int GetStudentsByTutorIDCount(Int32 schoolID, int tutorID, bool isCurrentPeriod, Boolean active);
        List<CourseDto> GetCourses(Int32 schoolID, Int32 enrollmentID, Boolean active);


        }
 }

 
