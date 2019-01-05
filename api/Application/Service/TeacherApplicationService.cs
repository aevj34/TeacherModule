using api.Application.Dto;
using api.Application.NotificationPattern;
using api.Common.Infrastructure.Security;
using api.Domain.Entity;
using api.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Application.Service
{
    public class TeacherApplicationService : BaseApplication
    {

        private readonly TeacherRepository teacherRepository;
        private JwtTokenProvider jwtTokenProvider;
        private readonly SchoolYearRepository schoolYearRepository;
        private readonly EvaluationPeriodRepository evaluationPeriodRepository;
        private readonly ProgrammingRepository programmingRepository;


        public TeacherApplicationService(TeacherRepository teacherRepository, SchoolYearRepository schoolYearRepository,
            EvaluationPeriodRepository evaluationPeriodRepository, ProgrammingRepository programmingRepository) : base()
        {
            this.teacherRepository = teacherRepository;
            this.jwtTokenProvider = new JwtTokenProvider();
            this.schoolYearRepository = schoolYearRepository;
            this.evaluationPeriodRepository = evaluationPeriodRepository;
            this.programmingRepository = programmingRepository;
        }

        public Object getCurrentCourses(Int32 schoolID, Int32 teacherID)
        {
            try
            {
                bool isActive = true;
                bool isCurrentPeriod = true;
  
                BaseResponseDto<ProgrammingListDto> baseResponseDto = new BaseResponseDto<ProgrammingListDto>();
                List<ProgrammingListDto> programmingDto = getCurrentCoursesByTeacherID(schoolID, teacherID, isActive, isCurrentPeriod);

                baseResponseDto.Data = programmingDto;
                return baseResponseDto;

            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }


        }



        public List<ProgrammingListDto> getCurrentCoursesByTeacherID(Int32 schoolID, Int32 teacherID, Boolean active, Boolean isCurrentPeriod)
        {

            BaseResponseDto<ProgrammingListDto> baseResponseDto = new BaseResponseDto<ProgrammingListDto>();
            List<ProgrammingListDto> programmingDto = this.programmingRepository.getCurrentCoursesByTeacherID(schoolID, teacherID, active, isCurrentPeriod);
            return programmingDto;

        }

        public int GetBycurrentYearByschoolIDByactive(Boolean isCurrentYear, Int32 schoolID, Boolean active)
        {
            BaseResponseDto<SchoolYearListDto> baseResponseDto = new BaseResponseDto<SchoolYearListDto>();
            List<SchoolYearListDto> schoolYearDto = this.schoolYearRepository.GetBycurrentYearByschoolIDByactive(isCurrentYear, schoolID, active);

            int currentSchoolYearID = 0;
            if (schoolYearDto.Count() > 0)
                currentSchoolYearID = schoolYearDto.ElementAt(0).schoolYearID;

            return currentSchoolYearID;
        }


        public int GetByactiveByisCurrentPeriodByschoolIDByschoolYearID(Boolean active, Boolean isCurrentPeriod, Int32 schoolID, Int32 schoolYearID)
        {

            BaseResponseDto<EvaluationPeriodListDto> baseResponseDto = new BaseResponseDto<EvaluationPeriodListDto>();
            List<EvaluationPeriodListDto> evaluationPeriodDto = this.evaluationPeriodRepository.GetByactiveByisCurrentPeriodByschoolIDByschoolYearID(active, isCurrentPeriod, schoolID, schoolYearID);

            int currentEvaluationPeriodID = 0;
            if (evaluationPeriodDto.Count() > 0)
                currentEvaluationPeriodID = evaluationPeriodDto.ElementAt(0).evaluationPeriodID;

            return currentEvaluationPeriodID;

        }

    }
}
