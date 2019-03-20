using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Dto;
using api.Application.Service;
using api.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using api.Common.Infrastructure.Security;
using Font = iTextSharp.text.Font;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowCors")]
    //[Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherApplicationService teacherApplicationService;

        public TeacherController()
        {
            this.teacherApplicationService = new TeacherApplicationService(new TeacherAdoNetRepository(), new SchoolYearAdoNet(), new EvaluationPeriodAdoNet(), new ProgrammingAdoNet(), new NoteAdoNet(), new NoteChangeAdoNet(), new EnrollmentDetailAdoNet(), new EvaluationAdoNet(), new EvaluationDetailAdoNet(), new EvaluationExpirationAdoNet(), new AssistanceAdoNet(), new AssistanceDetailAdoNet(), new AssistanceTypeAdoNet(), new StudentAdoNet());
        }

        [Route("GetByTeacherID")]
        public Object GetByTeacherID(int teacherID, int schoolID)
        {
            return teacherApplicationService.GetByTeacherID(teacherID, schoolID);
        }

        [Route("GetBystudentIDByactive")]
        public Object GetBystudentIDByactive(int studentID, Boolean active)
        {
            return teacherApplicationService.GetBystudentIDByactive(studentID, active);
        }

        [Route("getCurrentCourses")]
        public Object getCurrentCourses(Int32 schoolID, Int32 teacherID)
        {
            return teacherApplicationService.getCurrentCourses(schoolID, teacherID);
        }

        [Route("getCurrentCoursesToAssistance")]
        public Object getCurrentCoursesToAssistance(Int32 schoolID, Int32 teacherID)
        {
            return teacherApplicationService.getCurrentCoursesToAssistance(schoolID, teacherID);
        }

        [HttpGet]
        [Route("GetCoursesByStudent")]
        public Object GetCoursesByStudent(int studentID, Int32 schoolID, Int32 enrollmentID, Boolean active)
        {
            return teacherApplicationService.GetCoursesByStudent(studentID,schoolID, enrollmentID, active);
        }

        [HttpGet]
        [Route("GetCoursesByStudentTutor")]
        public Object GetCoursesByStudentTutor(Int32 schoolID, Int32 enrollmentID, Boolean active)
        {
            return teacherApplicationService.GetCoursesByStudentTutor(schoolID, enrollmentID, active);
        }


        [Route("getCurrentCoursesByTutorID")]
        public Object getCurrentCoursesByTutorID(Int32 schoolID, Int32 tutorID)
        {
            return teacherApplicationService.getCurrentCoursesByTutorID(schoolID, tutorID);
        }

        [Route("GetStudentsByCourse")]
        public Object GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID,int teacherTypeID)
        {
            return teacherApplicationService.GetStudentsByCourse(programmingID,schoolID,active,evaluationFormulaID, teacherTypeID);
        }

        [Route("GetStudentsByTutorID")]
        public Object GetStudentsByTutorID(Int32 schoolID, int tutorID, Boolean isCurrentPeriod, Boolean active, int skip, int pageSize, int selectedFooter, int footerSize)
        {
            return teacherApplicationService.GetStudentsByTutorID(schoolID, tutorID, isCurrentPeriod, active, skip, pageSize, selectedFooter, footerSize);
        }

        [Route("GetStudentsByCourseAssistance")]
        public Object GetStudentsByCourseAssistance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            return teacherApplicationService.GetStudentsByCourseAssistance(programmingID, schoolID, active);
        }

        [HttpPost("insertNotes")]
        [Route("insertNotes")]
        public Object insertNotes([FromBody] ProgrammingListDto requestProgramming)
        {
            return this.teacherApplicationService.insertNotes(requestProgramming);
        }

        [HttpPost("InsertAssistance")]
        [Route("InsertAssistance")]
        public Object InsertAssistance([FromBody] AssistanceInsertDto request)
        {
            return this.teacherApplicationService.InsertAssistance(request);
        }

        [HttpPut("UpdateHeaderAssistance")]
        [Route("UpdateHeaderAssistance")]
        public Object Put([FromBody] LessonDto lesson)
        {
            return this.teacherApplicationService.UpdateHeaderAssistance(lesson);
        }

        [HttpPut()]
        [Route("UpdateByassistanceID")]
        public Object UpdateByassistanceID(Int32 assistanceID, Boolean active)
        {
            return this.teacherApplicationService.UpdateByassistanceID(assistanceID, active);
        }

        [HttpPost()]
        [Route("InsertAssistance")]
        public Object InsertAssistance(int programmingID, int schoolID, string classTheme, string dateClass)
        {
            return this.teacherApplicationService.InsertAssistance(programmingID, schoolID, classTheme, dateClass);
        }

        [HttpGet]
        [Route("GetStudentsByCoursePDF")]
        public FileResult GetStudentsByCoursePDF(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCoursePDF(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);
            //return new FileStreamResult(ms,"application/pdf");
            return File(ms, "application/pdf", "Reporte_de_notas.pdf");
        }

        [HttpGet]
        [Route("GetStudentsByCourseWithoutNotesPDF")]
        public FileResult GetStudentsByCourseWithoutNotesPDF(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCourseWithoutNotesPDF(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);
            return File(ms, "application/pdf", "Registro_de_notas.pdf");
        }

        [HttpGet]
        [Route("GetStudentsByCourseWithoutNotesXLS")]
        public FileResult GetStudentsByCourseWithoutNotesXLS(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCourseWithoutNotesXLS(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);
            string fileName = @"Registro_de_notas.xlsx";
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }



        [HttpGet]
        [Route("GetStudentsByCourseWithoutNotesPDF_Assitance")]
        public FileResult GetStudentsByCourseWithoutNotesPDF_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCourseWithoutNotesPDF_Assitance(programmingID, schoolID, active);
            return File(ms, "application/pdf", "Registro_de_asistencia.pdf");
        }

        [HttpGet]
        [Route("GetStudentsByCoursePDF_Assitance")]
        public FileResult GetStudentsByCoursePDF_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCoursePDF_Assitance(programmingID, schoolID, active);
            //return new FileStreamResult(ms,"application/pdf");
            return File(ms, "application/pdf", "Reporte_de_asistencia.pdf");
        }


        [HttpGet]
        [Route("GetStudentsByCourseWithoutNotesXLS_Assitance")]
        public FileResult GetStudentsByCourseWithoutNotesXLS_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            MemoryStream ms = new MemoryStream();
            ms = this.teacherApplicationService.GetStudentsByCourseWithoutNotesXLS_Assitance(programmingID, schoolID, active);
            string fileName = @"Registro_de_asistencia.xlsx";
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


    }
}