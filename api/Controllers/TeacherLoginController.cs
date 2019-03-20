using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Dto;
using api.Application.Service;
using api.Infrastructure.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowCors")]
    public class TeacherLoginController : ControllerBase
    {

        private readonly TeacherLoginApplicationService teacherLoginApplicationService;

        public TeacherLoginController()
        {
            this.teacherLoginApplicationService = new TeacherLoginApplicationService(new TeacherAdoNetRepository(), new RoleInActionAdoNet());
        }

        [HttpPost("Login")]
        [Route("Login")]
        public Object Login([FromBody] TeacherDto requestLoginUserDto)
        {
            return teacherLoginApplicationService.validateTeacher(requestLoginUserDto);
        }


        [HttpGet]
        [Route("getMenus")]
        public Object getMenus(string dni, int schoolID)
        {
            return teacherLoginApplicationService.getMenus(dni, schoolID);
        }

    }
}