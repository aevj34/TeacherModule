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
    //[Authorize]
    public class EvaluationController : ControllerBase
    {

        private readonly EvaluationApplicationService evaluationApplicationService;
        public EvaluationController()
        {
            this.evaluationApplicationService = new EvaluationApplicationService(new EvaluationAdoNet());
        }
        [HttpGet]
        [Route("GetEvaluations")]
        public Object GetEvaluations(Int32 evaluationFormulaID, Int32 schoolID, Boolean active)
        {
            return evaluationApplicationService.GetEvaluations(evaluationFormulaID, schoolID, active);
        }


    }
}