
 using api.Application;
 using api.Application.Dto;
 using api.Domain.Repository;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using api.Common.Infrastructure.Security;
 namespace api.Application.Service
{
 public class EnrollmentDetailApplicationService: BaseApplication
 {
        private readonly EnrollmentDetailRepository enrollmentDetailRepository;
        private readonly EvaluationRepository evaluationRepository;
        private readonly NoteRepository noteRepository;

        public EnrollmentDetailApplicationService(EnrollmentDetailRepository enrollmentDetailRepository, EvaluationRepository evaluationRepository, NoteRepository noteRepository) : base()
        {
            this.enrollmentDetailRepository = enrollmentDetailRepository;
            this.evaluationRepository = evaluationRepository;
            this.noteRepository = noteRepository;
        }


        public Object GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
                evaluations = evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active);

                List<NoteListDto> notes = new List<NoteListDto>();
                notes = this.noteRepository.GetByprogrammingIDByschoolIDByactive(programmingID, schoolID, active);
      
                foreach (EnrollmentDetailListDto student in students)
                 {
                    List<NoteListDto> completeNotes = new List<NoteListDto>();   
                    //student.notes = notes.Where(e => e.studentID == student.studentID).ToList();

                    foreach (EvaluationListDto evaluationListDto in evaluations)
                    {
                        int noteID = 0;
                        string note = "";

                        NoteListDto obj = new NoteListDto();
                        obj = notes.Where(e => e.studentID == student.studentID && e.evaluationID == evaluationListDto.evaluationID).FirstOrDefault();

                        if (obj != null)
                        {
                            noteID = obj.noteID;
                            note = obj.note;
                        }
                    
                        NoteListDto completeNote = new NoteListDto();
                        completeNote.noteID = noteID;
                        completeNote.studentID = student.studentID;
                        completeNote.evaluationID = evaluationListDto.evaluationID;
                        completeNote.note = note;
                        completeNotes.Add(completeNote);
                    }
                    student.notes = completeNotes;
                 }

                baseResponseDto.Data = students;
                return baseResponseDto;

            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

    }
 }

 
