using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Dto
{
    public class EvaluationFormulaDto
    {
        public int evaluationFormulaID { get; set; }
        public List<EvaluationListDto> evaluations { get; set; }
        public List<EvaluationGroupDto> result { get; set; }
        public List<CourseDto> courses { get; set; }
    }
}
