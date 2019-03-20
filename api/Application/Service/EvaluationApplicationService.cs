
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
        private readonly EvaluationExpirationRepository evaluationExpirationRepository;
        public EvaluationApplicationService(EvaluationRepository evaluationRepository, EvaluationExpirationRepository evaluationExpirationRepository) : base()
 {
             this.evaluationRepository = evaluationRepository;
            this.evaluationExpirationRepository = evaluationExpirationRepository;
 }


  public Object GetEvaluations(Int32 evaluationFormulaID ,Int32 schoolID ,Boolean active, Int32 programmingID, int teacherTypeID)   {
    try 
    {
      BaseResponseDto<EvaluationListDto> baseResponseDto = new BaseResponseDto<EvaluationListDto>();
      List<EvaluationListDto> evaluationDto = this.evaluationRepository. GetEvaluations(evaluationFormulaID ,schoolID ,active, teacherTypeID) ;

                foreach (EvaluationListDto evaluationListDto in evaluationDto)
                {
                    ExpiredDto expired = new ExpiredDto();
                    expired = IsEvaluationExpired(programmingID, evaluationListDto.evaluationID);

                    evaluationListDto.isExpired = true;
                    evaluationListDto.ExpiredMessage = "";
                    evaluationListDto.ExpiredAfter = "";
                

                    if (expired != null)
                    {
                        evaluationListDto.isExpired = expired.isExpired;
                        evaluationListDto.ExpiredMessage = expired.expiredMessage;

                        if (expired.isExpired)
                        {
                            evaluationListDto.ExpiredAfter = expired.expiredAfter;
                        }
                        else
                        {
                            if (expired.isAboutExpired)
                            {
                                evaluationListDto.ExpiredAfter = expired.expiredAfter;
                            }
                        }
                      
                        evaluationListDto.isAboutExpired = expired.isAboutExpired;
                    }
                    else
                    {
                        evaluationListDto.ExpiredAfter = expired.expiredAfter;
                    }

                }

             baseResponseDto.Data = evaluationDto;
      return baseResponseDto;
    }
    catch ( Exception ex)
    {
      return this.getExceptionErrorResponse2(ex.Message);
    }
  }

        public Object GetEvaluationsHeader(Int32 evaluationFormulaID, Int32 schoolID, Boolean active, Int32 programmingID, int teacherTypeID)
        {
            try
            {
                BaseResponseDto<EvaluationGroupDto> baseResponseDto = new BaseResponseDto<EvaluationGroupDto>();
                List<EvaluationListDto> evaluations = this.evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active, teacherTypeID);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = GetEvaluationsHeaderFormated(evaluationFormulaID, schoolID, active, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;

                baseResponseDto.Data = result;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public ReturnListDto GetEvaluationsHeaderFormated(Int32 evaluationFormulaID, Int32 schoolID, Boolean active, int teacherTypeID)
        {
            List<EvaluationListDto> evaluations = this.evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active, teacherTypeID);

            List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();



            int count = 0;
            for (int i = 0; i <= evaluations.Count - 1; i++)
            {

                if (i == evaluations.Count - 1)
                {
                    EvaluationGroupDto obj = new EvaluationGroupDto();
                    obj.name = evaluations.ElementAt(i).evaluationType_name;
                    obj.colspan = count+1;
                    result.Add(obj);
                    break;
                }
                else
                {
                    for (int j = i + 1; j <= i + 1; j++)
                    {

                        count++;
                        if (evaluations.ElementAt(i).evaluationType_name != evaluations.ElementAt(j).evaluationType_name)
                        {     
                            EvaluationGroupDto obj = new EvaluationGroupDto();
                            obj.name = evaluations.ElementAt(i).evaluationType_name;
                            obj.colspan = count;
                            result.Add(obj);
                            count = 0;
                        }
                    }
                }

          

            }

            foreach (EvaluationGroupDto header in result)
            {
                if (header.colspan <= 0)
                {
                    header.colspan = 1;
                }
            }

            ReturnListDto returnListDto = new ReturnListDto();
            returnListDto.evaluations = evaluations;
            returnListDto.result = result;

            return returnListDto;

        }


        public ExpiredDto IsEvaluationExpired(Int32 programmingID, int evaluationID)
        {
            bool active = true;
            List<EvaluationExpirationListDto> expires = new List<EvaluationExpirationListDto>();
            expires = this.evaluationExpirationRepository.GetByprogrammingIDByactive(programmingID, active).Where(e => e.evaluationID == evaluationID).ToList();

            bool isExpired = true;
            string expiredMessage = "";
            string expiredAfter = "";
            bool isAboutExpired = false;

            if (expires.Count() == 0)
            {
                isExpired = true;
                expiredMessage = "La carga de notas para esta evaluación aún no está disponible.";
                expiredAfter = "Cerrado";
            }
            else
            {
                int i = expires.Where(e => DateTime.Now.Date < e.startDate || DateTime.Now.Date > e.endDate).Count();
                if (i > 0)
                {
                    EvaluationExpirationListDto evaluationExpired = new EvaluationExpirationListDto();
                    evaluationExpired = expires.ElementAt(0);

                    if (evaluationExpired != null)
                    {
                        expiredMessage = "La carga de notas para esta evaluación estuvo disponible desde el " + evaluationExpired.startDateShow + " hasta el " + evaluationExpired.endDateShow + ".";
                        expiredAfter = "Cerrado";
                    }

                    isExpired = true;
                }
                else
                {
                    EvaluationExpirationListDto evaluationExpired = new EvaluationExpirationListDto();
                    evaluationExpired = expires.ElementAt(0);
                    isExpired = false;
                    expiredMessage = "La carga de notas para esta evaluación se cerrará el " + evaluationExpired.endDateShow + ".";
                    expiredAfter = "Faltan " + TimerAgo.TimeAfter(expires.ElementAt(0).endDate.AddDays(1));
                    isAboutExpired = ValidAboutExpired(expires.ElementAt(0).endDate.AddDays(1));
                }
            }

            ExpiredDto expired = new ExpiredDto();
            expired.isExpired = isExpired;
            expired.expiredAfter = expiredAfter;
            expired.expiredMessage = expiredMessage;
            expired.isAboutExpired = isAboutExpired;

            return expired;
        }


        public bool ValidAboutExpired(DateTime date)
        {
            const int maxDays = 5;
            var timeSpan = date.Subtract(DateTime.Now);

            if (timeSpan.Days < maxDays)
                return true;
            else
                return false;
        }

        public Object GetEvaluationsLegend(Int32 evaluationFormulaID, Int32 schoolID, Boolean active)
        {
            try
            {
                BaseResponseDto<EvaluationListDto> baseResponseDto = new BaseResponseDto<EvaluationListDto>();
                List<EvaluationListDto> evaluationDto = this.evaluationRepository.GetEvaluationsLegend(evaluationFormulaID, schoolID, active);

                baseResponseDto.Data = evaluationDto;

                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

    }
 }

 
