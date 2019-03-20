
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
 namespace api.Domain.Repository
 {
 public class AssistanceApplicationService: BaseApplication
 {
    private readonly AssistanceRepository assistanceRepository;
    private readonly AssistanceTypeRepository assistanceTypeRepository;

 public AssistanceApplicationService(AssistanceRepository assistanceRepository, AssistanceTypeRepository assistanceTypeRepository) : base()
 {
 this.assistanceRepository = assistanceRepository;
  this.assistanceTypeRepository = assistanceTypeRepository;
 }


        public Object GetLessons(Int32 schoolID, Int32 programmingID, Boolean active)
        {
            try
            {
                BaseResponseDto<AssistanceListDto> baseResponseDto = new BaseResponseDto<AssistanceListDto>();
                List<AssistanceListDto> assistanceDto = this.assistanceRepository.GetLessons(schoolID, programmingID, active);
                baseResponseDto.Data = assistanceDto;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public Object GetAssistanceLegend(Int32 schoolID, Boolean active)
        {
            try
            {
                BaseResponseDto<AssistanceTypeListDto> baseResponseDto = new BaseResponseDto<AssistanceTypeListDto>();
                List<AssistanceTypeListDto> legends = this.assistanceTypeRepository.GetByschoolIDByactive(schoolID, active);
                baseResponseDto.Data = legends;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }



    }
}

 
