using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Dto;
using api.Application.Service;
using api.Infrastructure;
using api.Infrastructure.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowCors")]
    public class LoginController : ControllerBase
    {

        private readonly TeacherApplicationService teacherApplicationService;

        public LoginController()
        {
            this.teacherApplicationService = new TeacherApplicationService(new TeacherAdoNetRepository(), new RoleInActionAdoNet());
        }

        [HttpPost()]
        [Route("login")]
        public Object Login([FromBody] TeacherDto requestLoginUserDto)
        {

            return new string[] { "value1", "value2" };
           // return teacherApplicationService.validateTeacher(requestLoginUserDto);
        }


    }
}