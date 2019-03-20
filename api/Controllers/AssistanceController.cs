using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AssistanceController : ControllerBase
    {

        private readonly AssistanceApplicationService assistanceApplicationService;
        public AssistanceController()
        {
            this.assistanceApplicationService = new AssistanceApplicationService(new AssistanceAdoNet(), new AssistanceTypeAdoNet());
        }

        [HttpGet]
        [Route("GetLessons")]
        public Object GetLessons(Int32 schoolID, Int32 programmingID, Boolean active)
        {
            return assistanceApplicationService.GetLessons(schoolID, programmingID, active);
        }

        [HttpGet]
        [Route("GetAssistanceLegend")]
        public Object GetAssistanceLegend(Int32 schoolID, Boolean active)
        {
            return assistanceApplicationService.GetAssistanceLegend(schoolID, active);
        }

    }
}