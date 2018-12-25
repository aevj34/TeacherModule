using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Dto;
using api.Application.Service;
using api.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login2Controller : ControllerBase
    {

        private readonly TeacherApplicationService teacherApplicationService;

        public Login2Controller()
        {
            this.teacherApplicationService = new TeacherApplicationService(new TeacherAdoNetRepository(), new RoleInActionAdoNet());
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //shuuuuuu
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("Poste")]
        [Route("Poste")]
        public void Poste([FromBody] string value)
        {
        }

        [HttpPost("Login")]
        [Route("Login")]
        public Object Login([FromBody] TeacherDto requestLoginUserDto)
        {
            return teacherApplicationService.validateTeacher(requestLoginUserDto);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}