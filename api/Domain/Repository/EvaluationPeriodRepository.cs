using api.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Domain.Repository
{
    public interface EvaluationPeriodRepository
    {

        List<EvaluationPeriodListDto> GetByactiveByisCurrentPeriodByschoolIDByschoolYearID(Boolean active, Boolean isCurrentPeriod, Int32 schoolID, Int32 schoolYearID);

    }
}
