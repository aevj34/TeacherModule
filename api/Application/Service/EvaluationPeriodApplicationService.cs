
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;


 namespace api.Application.Service
{
 public class EvaluationPeriodApplicationService: BaseApplication
 {
 private readonly EvaluationPeriodRepository evaluationPeriodRepository;
 public EvaluationPeriodApplicationService(EvaluationPeriodRepository evaluationPeriodRepository) : base()
 {
 this.evaluationPeriodRepository = evaluationPeriodRepository;
 }





    }
}

 
