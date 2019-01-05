
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
 namespace api.Application.Service
{
 public class EvaluationApplicationService: BaseApplication
 {
 private readonly EvaluationRepository evaluationRepository;
 public EvaluationApplicationService(EvaluationRepository evaluationRepository) : base()
 {
 this.evaluationRepository = evaluationRepository;
 }


  public Object GetEvaluations(Int32 evaluationFormulaID ,Int32 schoolID ,Boolean active  )   {
    try 
    {
      BaseResponseDto<EvaluationListDto> baseResponseDto = new BaseResponseDto<EvaluationListDto>();
      List<EvaluationListDto> evaluationDto = this.evaluationRepository. GetEvaluations(evaluationFormulaID ,schoolID ,active) ;
      baseResponseDto.Data = evaluationDto;
      return baseResponseDto;
    }
    catch ( Exception ex)
    {
      return this.getExceptionErrorResponse2(ex.Message);
    }
  }

 }
 }

 
