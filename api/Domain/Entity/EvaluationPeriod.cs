using api.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Entity
{
    public class EvaluationPeriod
    {
        public String name { get; set; }
        public List<ProgrammingListDto> courses { get; set; }
    }
}
