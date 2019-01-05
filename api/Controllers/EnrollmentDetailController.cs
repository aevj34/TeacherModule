using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Application.Service;
using api.Domain.Repository;
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
    public class EnrollmentDetailController : ControllerBase
    {

        private readonly EnrollmentDetailApplicationService enrollmentDetailApplicationService;
        public EnrollmentDetailController()
        {
            this.enrollmentDetailApplicationService = new EnrollmentDetailApplicationService(new EnrollmentDetailAdoNet(), new EvaluationAdoNet(), new NoteAdoNet());
        }

        [HttpGet]
        [Route("GetStudentsByCourse")]
        public Object GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID)
        {
            return enrollmentDetailApplicationService.GetStudentsByCourse(programmingID, schoolID, active, evaluationFormulaID);
        }



    }
}