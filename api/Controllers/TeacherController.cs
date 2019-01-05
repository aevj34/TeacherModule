using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Service;
using api.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowCors")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherApplicationService teacherApplicationService;

        public TeacherController()
        {
            this.teacherApplicationService = new TeacherApplicationService(new TeacherAdoNetRepository(), new SchoolYearAdoNet(), new EvaluationPeriodAdoNet(), new ProgrammingAdoNet());
        }

        [Route("getCurrentCourses")]
        public Object getCurrentCourses(Int32 schoolID, Int32 teacherID)
        {
            return teacherApplicationService.getCurrentCourses(schoolID, teacherID);
        }


        [HttpPost("Login")]
        [Route("Login")]
        public Object saveNotes([FromBody] TeacherDto requestLoginUserDto)
        {
            return teacherLoginApplicationService.validateTeacher(requestLoginUserDto);
        }


    }
}