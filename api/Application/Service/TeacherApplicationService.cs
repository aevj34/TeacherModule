using api.Application.Dto;
using api.Application.NotificationPattern;
using api.Common.Infrastructure.Security;
using api.Domain.Entity;
using api.Domain.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace api.Application.Service
{
    public class TeacherApplicationService : BaseApplication
    {

        private readonly TeacherRepository teacherRepository;
        private JwtTokenProvider jwtTokenProvider;
        private readonly SchoolYearRepository schoolYearRepository;
        private readonly EvaluationPeriodRepository evaluationPeriodRepository;
        private readonly ProgrammingRepository programmingRepository;
        private readonly NoteRepository noteRepository;
        private readonly NoteChangeRepository noteChangeRepository;
        private readonly EnrollmentDetailRepository enrollmentDetailRepository;
        private readonly EvaluationRepository evaluationRepository;
        private readonly EvaluationDetailRepository evaluationDetailRepository;
        private readonly EvaluationExpirationRepository evaluationExpirationRepository;
        private readonly AssistanceRepository assistanceRepository;
        private readonly AssistanceDetailRepository assistanceDetailRepository;
        private readonly AssistanceTypeRepository assistanceTypeRepository;
        private readonly StudentRepository studentRepository;

        public TeacherApplicationService(TeacherRepository teacherRepository, SchoolYearRepository schoolYearRepository,
            EvaluationPeriodRepository evaluationPeriodRepository, ProgrammingRepository programmingRepository, 
            NoteRepository noteRepository, NoteChangeRepository noteChangeRepository, EnrollmentDetailRepository enrollmentDetailRepository,
            EvaluationRepository evaluationRepository, EvaluationDetailRepository evaluationDetailRepository,
            EvaluationExpirationRepository evaluationExpirationRepository,
            AssistanceRepository assistanceRepository, AssistanceDetailRepository assistanceDetailRepository,
            AssistanceTypeRepository assistanceTypeRepository,
            StudentRepository studentRepository) : base()
        {
            this.teacherRepository = teacherRepository;
            this.jwtTokenProvider = new JwtTokenProvider();
            this.schoolYearRepository = schoolYearRepository;
            this.evaluationPeriodRepository = evaluationPeriodRepository;
            this.programmingRepository = programmingRepository;
            this.noteRepository = noteRepository;
            this.noteChangeRepository = noteChangeRepository;
            this.enrollmentDetailRepository = enrollmentDetailRepository;
            this.evaluationRepository = evaluationRepository;
            this.evaluationDetailRepository = evaluationDetailRepository;
            this.evaluationExpirationRepository = evaluationExpirationRepository;
            this.assistanceRepository = assistanceRepository;
            this.assistanceDetailRepository = assistanceDetailRepository;
            this.assistanceTypeRepository = assistanceTypeRepository;
            this.studentRepository = studentRepository;
        }

        public Object GetByTeacherID(int teacherID, int schoolID)
        {
            try
            {
                BaseResponseDto<Teacher> baseResponseDto = new BaseResponseDto<Teacher>();
                Teacher teacher = this.teacherRepository.GetByTeacherID(teacherID, schoolID);
                baseResponseDto.single = teacher;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }


        }

        public Object GetBystudentIDByactive(Int32 studentID, Boolean active)
        {
            try
            {
                BaseResponseDto<StudentListDto> baseResponseDto = new BaseResponseDto<StudentListDto>();
                StudentListDto studentDto = this.studentRepository.GetBystudentIDByactive(studentID, active);
                baseResponseDto.single = studentDto;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }


        public Object GetCoursesByStudent(int studentID,Int32 schoolID, Int32 enrollmentID, Boolean active)
        {
            try
            {
                BaseResponseDto<EvaluationFormulaDto> baseResponseDto = new BaseResponseDto<EvaluationFormulaDto>();
                List<EvaluationFormulaDto> courses = new List<EvaluationFormulaDto>();

                courses = GetStudentWithNotes(studentID, schoolID,enrollmentID);

                baseResponseDto.Data = courses;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
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
                baseResponseDto.single = programmingDto.FirstOrDefault();
                return baseResponseDto;

            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }

        }


        public Object getCurrentCoursesToAssistance(Int32 schoolID, Int32 teacherID)
        {
            try
            {
                bool isActive = true;
                bool isCurrentPeriod = true;

                BaseResponseDto<ProgrammingListDto1> baseResponseDto = new BaseResponseDto<ProgrammingListDto1>();
                List<ProgrammingListDto1> programmingDto = this.programmingRepository.getCurrentCoursesByTeacherIDToAssistance(schoolID, teacherID, isActive, isCurrentPeriod);
               
                baseResponseDto.Data = programmingDto;
                baseResponseDto.single = programmingDto.FirstOrDefault();
                return baseResponseDto;

            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }


        }


        public Object getCurrentCoursesByTutorID(Int32 schoolID, Int32 tutorID)
        {
            try
            {
                bool isActive = true;
                bool isCurrentPeriod = true;

                BaseResponseDto<SectionDto> baseResponseDto = new BaseResponseDto<SectionDto>();
                List<ProgrammingListDto> programmingDto = this.programmingRepository.getCurrentCoursesByTutorID(schoolID, tutorID, isActive, isCurrentPeriod);

                IEnumerable<int> sectionsDistinc = programmingDto.Select(x => x.sectionID).Distinct();

                List<SectionDto> sections = new List<SectionDto>();

                foreach (int sec in sectionsDistinc)
                {
                    SectionDto section = new SectionDto();
                    section.sectionID = sec;

                    string sectionName = "";
                    ProgrammingListDto obj = programmingDto.Where(e => e.sectionID == sec).FirstOrDefault();
                    if (obj != null)
                    {
                        sectionName = obj.section_name;
                    }
                    section.name = sectionName;
                    section.courses = programmingDto.Where(e => e.sectionID == sec).ToList();
                    sections.Add(section);
                }

                    baseResponseDto.Data = sections.OrderBy(e => e.name).ToList();
                //baseResponseDto.single = programmingDto.FirstOrDefault();
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

        public Object insertNotes(ProgrammingListDto programmingCourse)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    List<EnrollmentDetailListDto> students = new List<EnrollmentDetailListDto>();
                    students = programmingCourse.students;

                    if (students != null)
                    {
                        foreach (EnrollmentDetailListDto student in students)
                        {
                            if (student.notes != null)
                            {
                                foreach (NoteListDto noteListDto in student.notes)
                                {
                                    Note note = new Note();

                                    note.noteID = noteListDto.noteID;
                                    note.enrollmentID = student.enrollmentID;
                                    note.enrollmentDetailID = noteListDto.enrollmentDetailID;

                                    if (IsNumeric(noteListDto.note))
                                        note.note = noteListDto.note;
                                    else
                                        note.note = "100";

                                    note.studentID = noteListDto.studentID;
                                    note.courseID = programmingCourse.courseID;
                                    note.schoolID = programmingCourse.schoolID;
                                    note.headquartersID = programmingCourse.headquartersID;
                                    note.careerID = programmingCourse.careerID;
                                    note.schoolYearID = programmingCourse.schoolYearID;
                                    note.programmingID = programmingCourse.programmingID;
                                    note.evaluationPeriodID = programmingCourse.evaluationPeriodID;
                                    note.turnID = programmingCourse.turnID;
                                    note.sectionID = programmingCourse.sectionID;
                                    note.periodTypeID = programmingCourse.periodTypeID;
                                    note.evaluationID = noteListDto.evaluationID;
                                    note.grade = programmingCourse.grade;
                                    bool isActive = true;
                                    note.active = isActive;

                                    int noteID = 0;
                                    if (noteListDto.noteID == 0)
                                    {
                                        if (note.note != "100")
                                        {
                                            if (noteListDto.isAverage == false)
                                            {
                                                noteID = this.noteRepository.Insert(note);
                                                InsertChangeNote(programmingCourse, noteID, note.note);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        noteID = note.noteID;
                                        if (note.note != "100")
                                        {
                                            if (noteListDto.isChanged)
                                            {
                                                if (noteListDto.isAverage == false)
                                                {
                                                    this.noteRepository.Update(note);
                                                    InsertChangeNote(programmingCourse, noteID, note.note);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (noteListDto.note == "")
                                            {
                                                if (noteListDto.isChanged)
                                                {
                                                    if (IsNumeric(noteListDto.lastNote))
                                                    {
                                                        this.noteRepository.UpdateWhiteNoteBynoteID(noteID);
                                                        string nullNote = "100";
                                                        InsertChangeNote(programmingCourse, noteID, nullNote);
                                                    }
                                                }
                                            }

                                        }


                                    }


                                }
                            }
                        }
                    }
                    scope.Complete();
                }

                bool isStudentActive = true;
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> studentsWithNotes = GetStudents(programmingCourse.programmingID, programmingCourse.schoolID, isStudentActive, programmingCourse.evaluationFormulaID, programmingCourse.teacherTypeID);

                baseResponseDto.Data = studentsWithNotes;
                return baseResponseDto;

            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }

        }

        public void InsertChangeNote(ProgrammingListDto programmingCourse, int noteID, string note)
        {
            NoteChangeDto noteChangeDto = new NoteChangeDto();
            noteChangeDto.noteID = noteID;
            noteChangeDto.note = note;
            noteChangeDto.teacherID = programmingCourse.teacherID;
            noteChangeDto.userID = 0;
            noteChangeDto.changeDate = DateTime.Now.Date;
            noteChangeDto.schoolID = programmingCourse.schoolID;
             this.noteChangeRepository.Insert(noteChangeDto);
        }

        public static Boolean IsNumeric(string valor)
        {
            decimal result;
            return decimal.TryParse(valor, out result);
        }


        public Object GetStudentsByCourse(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> students = GetStudents(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);
                baseResponseDto.Data = students;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public Object GetStudentsByCourseAssistance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            try
            {
                BaseResponseDto<StudentAssistanceDto> baseResponseDto = new BaseResponseDto<StudentAssistanceDto>();
                List<StudentAssistanceDto> students = GetStudentsAssistance(programmingID, schoolID, active);
                baseResponseDto.Data = students;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public Object GetStudentsByTutorID( Int32 schoolID, int tutorID, Boolean isCurrentPeriod, Boolean active, int skip, int pageSize, int selectedFooter, int footerSize)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByTutorID(schoolID,tutorID,isCurrentPeriod,active,skip,pageSize);

                for (int i = 0; i <= students.Count - 1; i++)
                    students.ElementAt(i).orderNumber = i + 1;

                baseResponseDto.Data = students;
                FactoryPaginator<EnrollmentDetailListDto>.setPaginator(this.enrollmentDetailRepository.GetStudentsByTutorIDCount(schoolID,tutorID,isCurrentPeriod,active), pageSize, selectedFooter, footerSize, baseResponseDto);

                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }


        public Object GetCoursesByStudentTutor(Int32 schoolID, int enrollmentID, Boolean active)
        {
            try
            {
                BaseResponseDto<CourseDto> baseResponseDto = new BaseResponseDto<CourseDto>();
                List<CourseDto> courses = this.enrollmentDetailRepository.GetCourses(schoolID, enrollmentID, active);

                courses = courses.OrderBy(e => e.course_name).ToList();

                for (int i = 0; i <= courses.Count - 1; i++)
                    courses.ElementAt(i).orderNumber = i + 1;

                baseResponseDto.Data = courses;
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public List<EvaluationFormulaDto> GetStudentWithNotes(int studentID, Int32 schoolID,int enrollmentID)
        {
            const int teacherTypeID = 1;
            const bool isActive = true;

            List<EvaluationFormulaDto> formulas = new List<EvaluationFormulaDto>();

            List<CourseDto> courses = this.enrollmentDetailRepository.GetCourses(schoolID, enrollmentID, isActive);

            List<TutorNoteDto> notes = new List<TutorNoteDto>();
            notes = this.noteRepository.GetNotesByStudent(enrollmentID,studentID, schoolID, isActive);

            SchoolYearListDto schoolYear = new SchoolYearListDto();

            foreach (CourseDto course in courses)
            {
                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
                evaluations = evaluationRepository.GetEvaluations(course.evaluationFormulaID, schoolID, isActive, teacherTypeID);

                course.evaluations = evaluations;

                List<TutorNoteDto> completeNotes = new List<TutorNoteDto>();

                foreach (EvaluationListDto evaluationListDto in evaluations)
                {
                    int noteID = 0;
                    string note = "";
                    string lastNote = "";
                    int schoolYearID = 0;

                    TutorNoteDto obj = new TutorNoteDto();
                    obj = notes.Where(e => e.courseID == course.courseID && e.evaluationID == evaluationListDto.evaluationID).FirstOrDefault();

                    if (obj != null)
                    {
                        noteID = obj.noteID;
                        note = obj.note;
                        lastNote = obj.note;
                        schoolYearID = obj.schoolYearID;
                    }

                    TutorNoteDto completeNote = new TutorNoteDto();
                    completeNote.noteID = noteID;

                        schoolYear = this.schoolYearRepository.Obtain(schoolYearID);
                        int minimumNote = 11;
                        if (schoolYear != null)
                            minimumNote = schoolYear.minimumNote;


                        if (evaluationListDto.isAverage)
                        {
                            completeNote.note = GetAverageTutor(evaluationListDto.evaluationID, notes.Where(e => e.courseID == course.courseID).ToList());
                            completeNote.isAverage = true;
                        }
                        else
                        {
                            completeNote.note = note;
                            completeNote.isAverage = false;
                        }

                        if (IsNumeric(completeNote.note))
                        {
                            if (decimal.Parse(completeNote.note) >= minimumNote)
                                completeNote.isApproved = true;
                            else
                                completeNote.isApproved = false;
                        }
                        else
                        {
                            completeNote.isApproved = true;
                        }

                    completeNotes.Add(completeNote);
                }

                course.notes = completeNotes;
            }

            courses = courses.OrderBy(e => e.course_name).ToList();

            for (int i = 0; i <= courses.Count - 1; i++)
                courses.ElementAt(i).orderNumber = i + 1;

            IEnumerable<int> evaluationFormulaDistinc = courses.Select(x => x.evaluationFormulaID).Distinct();

            foreach(int formulaID in evaluationFormulaDistinc)
            {
                EvaluationApplicationService evaluationApplicationService = new EvaluationApplicationService(this.evaluationRepository, this.evaluationExpirationRepository);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = evaluationApplicationService.GetEvaluationsHeaderFormated(formulaID, schoolID, isActive, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;

                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
                evaluations = returnListDto.evaluations;

                EvaluationFormulaDto evaluationFormulaDto = new EvaluationFormulaDto();
                evaluationFormulaDto.evaluationFormulaID = formulaID;
                evaluationFormulaDto.evaluations = evaluations;
                evaluationFormulaDto.result = result;
                evaluationFormulaDto.courses = courses.Where(e => e.evaluationFormulaID == formulaID).ToList();
                formulas.Add(evaluationFormulaDto);
            }

            return formulas;
        }


        public List<EvaluationFormulaDto> GetStudentWithNotesAssistance(int studentID, Int32 schoolID, int enrollmentID)
        {
            const int teacherTypeID = 1;
            const bool isActive = true;

            List<EvaluationFormulaDto> formulas = new List<EvaluationFormulaDto>();

            List<CourseDto> courses = this.enrollmentDetailRepository.GetCourses(schoolID, enrollmentID, isActive);

            List<TutorNoteDto> notes = new List<TutorNoteDto>();
            notes = this.noteRepository.GetNotesByStudent(enrollmentID, studentID, schoolID, isActive);

            SchoolYearListDto schoolYear = new SchoolYearListDto();

            List<AssistanceDetailDto> assistances = new List<AssistanceDetailDto>();
            assistances = this.assistanceDetailRepository.GetAssistanceDetailByStudent(enrollmentID, schoolID, isActive);

            foreach (CourseDto course in courses)
            {

                List<AssistanceListDto> lessons = new List<AssistanceListDto>();
                lessons = this.assistanceRepository.GetLessons(schoolID, course.programmingID, isActive);

                List<AssistancexStudentDto> completeAssistances = new List<AssistancexStudentDto>();

                foreach (AssistanceListDto lesson in lessons)
                {

                    int assistanceDetailID = 0;
                    string typeAssistance = "";
                    int assistanceTypeID = 0;

                    AssistanceDetailDto obj = new AssistanceDetailDto();
                    obj = assistances.Where(e => e.programmingID == course.programmingID && e.assistanceID == lesson.assistanceID).FirstOrDefault();

                    if (obj != null)
                    {
                        assistanceDetailID = obj.assistanceDetailID;
                        typeAssistance = obj.assistanceType_abbreviation;
                        assistanceTypeID = obj.assistanceTypeID;
                    }
                    else
                    {
                        //if (isLackAssistance != null)
                        //{
                        //    typeAssistance = isLackAssistance.abbreviation;
                        //    assistanceTypeID = isLackAssistance.assistanceTypeID;
                        //}
                    }

                    AssistancexStudentDto completeAssistance = new AssistancexStudentDto();
                    completeAssistance.assistanceDetailID = assistanceDetailID;
                    completeAssistance.assistanceID = lesson.assistanceID;
                    completeAssistance.assistanceType_abbreviation = typeAssistance;
                    completeAssistance.assistanceTypeID = assistanceTypeID;

                    completeAssistances.Add(completeAssistance);
                }

                course.lessons = lessons;
                course.completeAssistances = completeAssistances;

            }

            courses = courses.OrderBy(e => e.course_name).ToList();

            for (int i = 0; i <= courses.Count - 1; i++)
                courses.ElementAt(i).orderNumber = i + 1;

            IEnumerable<int> evaluationFormulaDistinc = courses.Select(x => x.evaluationFormulaID).Distinct();

            foreach (int formulaID in evaluationFormulaDistinc)
            {
                EvaluationApplicationService evaluationApplicationService = new EvaluationApplicationService(this.evaluationRepository, this.evaluationExpirationRepository);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = evaluationApplicationService.GetEvaluationsHeaderFormated(formulaID, schoolID, isActive, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;

                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
                evaluations = returnListDto.evaluations;

                EvaluationFormulaDto evaluationFormulaDto = new EvaluationFormulaDto();
                evaluationFormulaDto.evaluationFormulaID = formulaID;
                evaluationFormulaDto.evaluations = evaluations;
                evaluationFormulaDto.result = result;
                evaluationFormulaDto.courses = courses.Where(e => e.evaluationFormulaID == formulaID).ToList();
                formulas.Add(evaluationFormulaDto);
            }

            return formulas;
        }


        public List<EnrollmentDetailListDto>  GetStudents(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

            ProgrammingDto programmingDto = new ProgrammingDto();
            programmingDto = this.programmingRepository.Obtain(programmingID);

            List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
            evaluations = evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active, teacherTypeID);

            SchoolYearListDto schoolYear = new SchoolYearListDto();
     
            List<NoteListDto> notes = new List<NoteListDto>();
            notes = this.noteRepository.GetByprogrammingIDByschoolIDByactive(programmingID, schoolID, active);


            foreach (EnrollmentDetailListDto student in students)
            {
                student.evaluations = evaluations.Count();

                List<NoteListDto> completeNotes = new List<NoteListDto>();

                foreach (EvaluationListDto evaluationListDto in evaluations)
                {
                     evaluationListDto.isExpired = IsEvaluationExpired(programmingID, evaluationListDto.evaluationID);
                     
                    int noteID = 0;
                    string note = "";
                    string lastNote = "";

                    NoteListDto obj = new NoteListDto();
                    obj = notes.Where(e => e.studentID == student.studentID && e.evaluationID == evaluationListDto.evaluationID).FirstOrDefault();

                    if (obj != null)
                    {
                        noteID = obj.noteID;
                        note = obj.note;
                        lastNote = obj.note;
                    }

                    NoteListDto completeNote = new NoteListDto();
                    completeNote.noteID = noteID;

                    if (programmingDto != null)
                    {
                        schoolYear = this.schoolYearRepository.Obtain(programmingDto.schoolYearID);
                        int minimumNote = 11;
                        if (schoolYear != null)
                            minimumNote = schoolYear.minimumNote;

                        completeNote.evaluationID = evaluationListDto.evaluationID;
                        completeNote.enrollmentID = student.enrollmentID;
                        completeNote.enrollmentDetailID = student.enrollmentDetailID;
                      
                        if (evaluationListDto.isAverage)
                        {
                            completeNote.note = GetAverage(evaluationListDto.evaluationID, notes.Where(e => e.studentID == student.studentID).ToList());
                            completeNote.lastNote = completeNote.note;
                            completeNote.isAverage = true;
                        }
                        else
                        {
                            completeNote.note = note;
                            completeNote.lastNote = note;
                            completeNote.isAverage = false;
                        }

                        if (IsNumeric(completeNote.note))
                        {
                            if (decimal.Parse(completeNote.note) >= minimumNote)
                                completeNote.isApproved = true;
                            else
                                completeNote.isApproved = false;
                        }
                        else
                        {
                            completeNote.isApproved = true;
                        }

                        //completeNote.isExpired = IsEvaluationExpired(programmingID, evaluationListDto.evaluationID);

                        if (completeNote.isAverage)
                        {
                            completeNote.isInputText = false;
                        }
                        else
                        {
                            if (evaluationListDto.isExpired)
                            {
                                completeNote.isInputText = false;
                            }
                            else
                            {
                                completeNote.isInputText = true;
                            }
                        }

                        completeNote.studentID = student.studentID;

                        completeNote.courseID = programmingDto.courseID;
                        completeNote.schoolID = programmingDto.schoolID;
                        completeNote.headquartersID = programmingDto.headquartersID;

                        completeNote.careerID = programmingDto.careerID;
                        completeNote.schoolYearID = programmingDto.schoolYearID;
                        completeNote.programmingID = programmingID;

                        completeNote.evaluationPeriodID = programmingDto.evaluationPeriodID;
                        completeNote.turnID = programmingDto.turnID;
                        completeNote.sectionID = programmingDto.sectionID;

                        completeNote.periodTypeID = programmingDto.periodTypeID;
                        completeNote.grade = programmingDto.grade;
                        completeNote.isChanged = false;
                        completeNote.active = active;
                    }

                    completeNotes.Add(completeNote);
                }
                student.notes = completeNotes;
            }

            students = students.OrderBy(e => e.student_firstName).ThenBy(e => e.student_lastName).ThenBy(e => e.student_name).ToList();

            for (int i=0; i<=students.Count-1; i++)
                students.ElementAt(i).orderNumber = i + 1;

            return students;
        }


        public List<StudentAssistanceDto> GetStudentsAssistance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            List<EnrollmentDetailListDto> enrollmentDetails = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

            List<StudentAssistanceDto> students = new List<StudentAssistanceDto>();
            foreach(EnrollmentDetailListDto enrollmentDetail in enrollmentDetails)
            {
                StudentAssistanceDto student = new StudentAssistanceDto();
                student.studentID = enrollmentDetail.studentID;
                student.student_code = enrollmentDetail.student_code;
                student.student_name = enrollmentDetail.student_name;
                student.student_firstName = enrollmentDetail.student_firstName;
                student.student_lastName = enrollmentDetail.student_lastName;
                student.EndPoint = enrollmentDetail.EndPoint;
                students.Add(student);
            }

            ProgrammingDto programmingDto = new ProgrammingDto();
            programmingDto = this.programmingRepository.Obtain(programmingID);

            List<AssistanceListDto> lessons = new List<AssistanceListDto>();
            lessons = this.assistanceRepository.GetLessons(schoolID, programmingID, active);

            List<AssistanceDetailDto> assistances = new List<AssistanceDetailDto>();
            assistances = this.assistanceDetailRepository.GetByprogrammingIDByschoolIDByactive(programmingID, schoolID, active);

            List<AssistanceTypeListDto> assistanceTypes = new List<AssistanceTypeListDto>();
            assistanceTypes = this.assistanceTypeRepository.GetByschoolIDByactive(schoolID, active);

            AssistanceTypeListDto isLackAssistance = new AssistanceTypeListDto();
            isLackAssistance = assistanceTypes.Where(e => e.isLack).FirstOrDefault();

            foreach (StudentAssistanceDto student in students)
            {
                List<AssistancexStudentDto> completeAssistances = new List<AssistancexStudentDto>();

                foreach (AssistanceListDto lesson in lessons)
                {
                   
                    int assistanceDetailID = 0;
                    string typeAssistance = "";
                    int assistanceTypeID = 0;

                    AssistanceDetailDto obj = new AssistanceDetailDto();
                    obj = assistances.Where(e => e.studentID == student.studentID && e.assistanceID == lesson.assistanceID).FirstOrDefault();

                    if (obj != null)
                    {
                        assistanceDetailID = obj.assistanceDetailID;
                        typeAssistance = obj.assistanceType_abbreviation;
                        assistanceTypeID = obj.assistanceTypeID;
                    }
                    else
                    {
                        if (isLackAssistance != null)
                        {
                            typeAssistance = isLackAssistance.abbreviation;
                            assistanceTypeID = isLackAssistance.assistanceTypeID;
                        }
                    }

                    AssistancexStudentDto completeAssistance = new AssistancexStudentDto();
                    completeAssistance.assistanceDetailID = assistanceDetailID;
                    completeAssistance.assistanceID = lesson.assistanceID;
                    completeAssistance.assistanceType_abbreviation = typeAssistance;
                    completeAssistance.assistanceTypeID = assistanceTypeID;

                    completeAssistances.Add(completeAssistance);
                }

                student.assistances = completeAssistances;
                student.assistanceTypes = assistanceTypes;

            }

            students = students.OrderBy(e => e.student_firstName).ThenBy(e => e.student_lastName).ThenBy(e => e.student_name).ToList();

            for (int i = 0; i <= students.Count - 1; i++)
                students.ElementAt(i).orderNumber = i + 1;

            return students;
        }


        public Object UpdateHeaderAssistance(LessonDto lesson)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();

                  Assistance assistance = new Assistance();
                  assistance = CreateAssistance(lesson.programmingID, lesson.schoolID, lesson.classTheme, lesson.dateClassShow);
                  assistance.assistanceID = lesson.assistanceID;

                  this.assistanceRepository.Update(assistance);

                return baseResponseDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Object UpdateByassistanceID(Int32 assistanceID, Boolean active)
        {
            try
            {
                BaseResponseDto<AssistanceDto> baseResponseDto = new BaseResponseDto<AssistanceDto>();
                this.assistanceRepository.UpdateByassistanceID(assistanceID, active);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public Object InsertAssistance(int programmingID, int schoolID, string classTheme, string dateClass)
        {
            try
            {
                BaseResponseDto<AssistanceDto> baseResponseDto = new BaseResponseDto<AssistanceDto>();

                Assistance assistance = new Assistance();
                assistance = CreateAssistance(programmingID, schoolID, classTheme, dateClass);

                this.assistanceRepository.Insert(assistance);

                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return this.getExceptionErrorResponse2(ex.Message);
            }
        }

        public Object InsertAssistance(AssistanceInsertDto request)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
      
                    foreach (AssistanceListDto lesson in request.lessons)
                    {
                        Assistance assistance = new Assistance();
                        assistance = CreateAssistance(request.programmingID, request.schoolID, lesson.classTheme, lesson.dateClassShow);

                        int assistanceID = 0;
                        if (lesson.assistanceID == 0)
                        {
                            assistanceID = this.assistanceRepository.Insert(assistance);
                        }
                        else
                        {
                            assistanceID = lesson.assistanceID;
                            assistance.assistanceID = assistanceID;
                            this.assistanceRepository.Update(assistance);
                        }

                        if (request.students != null)
                        {
                            foreach (StudentAssistanceDto student in request.students)
                            {

                                if (student.assistances != null)
                                {
                                    foreach (AssistancexStudentDto assistencexStudent in student.assistances)
                                    {
                                        if (assistencexStudent.assistanceID == lesson.assistanceID)
                                        {
                                            AssistanceDetailDto detail = new AssistanceDetailDto();
                                            detail = CreateAssistanceDetail(request.programmingID, request.schoolID, assistencexStudent.assistanceTypeID, student.studentID);
                                            detail.assistanceID = assistanceID;

                                            int assistanceDetailID = 0;
                                            if (assistencexStudent.assistanceDetailID == 0)
                                            {
                                                assistanceDetailID = this.assistanceDetailRepository.Insert(detail);
                                            }
                                            else
                                            {
                                                assistanceDetailID = assistencexStudent.assistanceDetailID;
                                                detail.assistanceDetailID = assistanceDetailID;
                                                this.assistanceDetailRepository.Update(detail);
                                            }
                                        }

                                    }
                                }

                            }
                        }
     


                    }


                    scope.Complete();
                }

                //bool isStudentActive = true;
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                //List<EnrollmentDetailListDto> studentsWithNotes = GetStudents(programmingCourse.programmingID, programmingCourse.schoolID, isStudentActive, programmingCourse.evaluationFormulaID, programmingCourse.teacherTypeID);

                //baseResponseDto.Data = studentsWithNotes;
                return baseResponseDto;

            }
            catch (Exception ex)
            {
               throw ex;
            }

        }

        Assistance CreateAssistance(int programmingID, int schoolID, string classTheme, string dateClass)
        {
            Assistance assistance = new Assistance();

            ProgrammingDto programming = new ProgrammingDto();
            programming = this.programmingRepository.Obtain(programmingID);

            if (programming != null)
            {
                assistance.classTheme = classTheme;
                assistance.dateClass = dateClass;
                assistance.observation = "";
                assistance.schoolID = schoolID;
                assistance.careerID = programming.careerID;
                assistance.courseID = programming.courseID;
                assistance.schoolYearID = programming.schoolYearID;
                assistance.classroomID = programming.classroomID;
                assistance.headquartersID = programming.headquartersID;
                assistance.programmingID = programming.programmingID;
                assistance.teacherID = programming.teacherID;
                assistance.turnID = programming.turnID;
                assistance.sectionID = programming.sectionID;
                assistance.evaluationPeriodID = programming.evaluationPeriodID;
                bool isActive = true;
                assistance.active = isActive;
            }
   
            return assistance;
        }

        AssistanceDetailDto CreateAssistanceDetail(int programmingID, int schoolID, int assistanceTypeID, int studentID)
        {
            AssistanceDetailDto assistance = new AssistanceDetailDto();

            ProgrammingDto programming = new ProgrammingDto();
            programming = this.programmingRepository.Obtain(programmingID);

            if (programming != null)
            {
                assistance.assistanceTypeID = assistanceTypeID;
                assistance.studentID = studentID;
                assistance.schoolID = schoolID;
                assistance.careerID = programming.careerID;
                assistance.courseID = programming.courseID;
                assistance.schoolYearID = programming.schoolYearID;
                assistance.classroomID = programming.classroomID;
                assistance.headquartersID = programming.headquartersID;
                assistance.programmingID = programming.programmingID;
                assistance.teacherID = programming.teacherID;
                assistance.turnID = programming.turnID;
                assistance.sectionID = programming.sectionID;

             bool isActive = true;
                assistance.active = isActive;
            }

            return assistance;
        }

        AssistanceDetailDto CreateAssistanceDetail(ProgrammingListDto programmingCourse,int schoolID, int studentID)
        {
            AssistanceDetailDto assistanceDetail = new AssistanceDetailDto();
            assistanceDetail.assistanceID = programmingCourse.assistanceID;
            assistanceDetail.assistanceTypeID = programmingCourse.assistanceTypeID;
            assistanceDetail.schoolID = schoolID;
            assistanceDetail.studentID = studentID;
            assistanceDetail.careerID = programmingCourse.careerID;
            assistanceDetail.courseID = programmingCourse.courseID;
            assistanceDetail.schoolYearID = programmingCourse.schoolYearID;
            assistanceDetail.classroomID = programmingCourse.classroomID;
            assistanceDetail.headquartersID = programmingCourse.headquartersID;
            assistanceDetail.programmingID = programmingCourse.programmingID;
            assistanceDetail.teacherID = programmingCourse.teacherID;
            assistanceDetail.turnID = programmingCourse.turnID;
            assistanceDetail.sectionID = programmingCourse.sectionID;
            bool isActive = true;
            assistanceDetail.active = isActive;
            return assistanceDetail;
        }

        public bool IsEvaluationExpired(Int32 programmingID, int evaluationID)
        {
            bool active = true;
            List<EvaluationExpirationListDto> expires = new List<EvaluationExpirationListDto>();
            expires = this.evaluationExpirationRepository.GetByprogrammingIDByactive(programmingID, active).Where(e => e.evaluationID == evaluationID).ToList();

            bool isExpired = true;
            if (expires.Count() == 0)
            {
                isExpired = true;
            }
            else
            {
                int i = expires.Where(e => DateTime.Now.Date < e.startDate || DateTime.Now.Date > e.endDate).Count();
                if (i > 0)
                {
                    isExpired = true;
                }
                else
                    isExpired = false;
            }

            return isExpired;
        }

        public List<EnrollmentDetailListDto> GetStudentsWithoutNotes(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

            ProgrammingDto programmingDto = new ProgrammingDto();
            programmingDto = this.programmingRepository.Obtain(programmingID);

            List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
            evaluations = evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active,teacherTypeID);

            SchoolYearListDto schoolYear = new SchoolYearListDto();


            foreach (EnrollmentDetailListDto student in students)
            {
                student.evaluations = evaluations.Count();

                List<NoteListDto> completeNotes = new List<NoteListDto>();

                foreach (EvaluationListDto evaluationListDto in evaluations)
                {

                    NoteListDto completeNote = new NoteListDto();

                    if (programmingDto != null)
                    {
                        schoolYear = this.schoolYearRepository.Obtain(programmingDto.schoolYearID);
                        int minimumNote = 11;
                        if (schoolYear != null)
                            minimumNote = schoolYear.minimumNote;

                        completeNote.evaluationID = evaluationListDto.evaluationID;
                            completeNote.note = "";
                            completeNote.isAverage = false;
                        completeNote.isChanged = false;
                        completeNote.active = active;
                    }

                    completeNotes.Add(completeNote);
                }
                student.notes = completeNotes;
            }

            students = students.OrderBy(e => e.student_firstName).ThenBy(e => e.student_lastName).ThenBy(e => e.student_name).ToList();

            for (int i = 0; i <= students.Count - 1; i++)
                students.ElementAt(i).orderNumber = i + 1;

            return students;
        }

        string GetAverage(int evaluationAverageID, List<NoteListDto> notesByStudents)
        {
            List<EvaluationDetailListDto> evaluations = new List<EvaluationDetailListDto>();
            evaluations = this.evaluationDetailRepository.GetEvaluationsByEvaluationAverageID( evaluationAverageID);

            decimal average = 0;

            foreach (EvaluationDetailListDto evaluation in evaluations)
            {
                NoteListDto note2 = new NoteListDto();
                note2 = notesByStudents.Where(e => e.evaluationID == evaluation.evaluationID).FirstOrDefault();
                if (note2 != null)
                {
                    decimal note = 0;
                    if (IsNumeric(note2.note))
                    {
                        note = decimal.Parse(note2.note);
                    }
                    average = average + note * evaluation.evaluation_weight;
                }

            }

            string stringAverage = "";
            if (average == 0)
                stringAverage = "-";
            else
                stringAverage = Math.Round(average, 2).ToString();

            return stringAverage;

        }

        string GetAverageTutor(int evaluationAverageID, List<TutorNoteDto> notesByStudents)
        {
            List<EvaluationDetailListDto> evaluations = new List<EvaluationDetailListDto>();
            evaluations = this.evaluationDetailRepository.GetEvaluationsByEvaluationAverageID(evaluationAverageID);

            decimal average = 0;

            foreach (EvaluationDetailListDto evaluation in evaluations)
            {
                TutorNoteDto note2 = new TutorNoteDto();
                note2 = notesByStudents.Where(e => e.evaluationID == evaluation.evaluationID).FirstOrDefault();
                if (note2 != null)
                {
                    decimal note = 0;
                    if (IsNumeric(note2.note))
                    {
                        note = decimal.Parse(note2.note);
                    }
                    average = average + note * evaluation.evaluation_weight;
                }

            }

            string stringAverage = "";
            if (average == 0)
                stringAverage = "-";
            else
                stringAverage = Math.Round(average, 2).ToString();

            return stringAverage;

        }

        public MemoryStream GetStudentsByCoursePDF(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> students = GetStudents(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);

                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();

                EvaluationApplicationService evaluationApplicationService = new EvaluationApplicationService(this.evaluationRepository, this.evaluationExpirationRepository);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = evaluationApplicationService.GetEvaluationsHeaderFormated(evaluationFormulaID, schoolID, active, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;
                evaluations = returnListDto.evaluations;

                ProgrammingDto programing = new ProgrammingDto();
                programing = this.programmingRepository.ObtainPDF(programmingID);

                string title = "REPORTE DE NOTAS";
                string schoolName = "";
                string subTitle = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";

                if (programing != null)
                {
                    schoolName = programing.school_name;
                    subTitle = programing.career_name;
                    header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                    header2 = "Curso: " + programing.course_name;
                    header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
                }

                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.Letter, 30F, 20f, 170f, 40f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);
                pw.PageEvent = new HeadeFooter(title, subTitle, schoolName, header1, header2, header3);

                document.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
                Font fontText = new Font(bf, 10, 0, BaseColor.Black);

                const int INIT = 2;
                int columns = 0;
                columns = INIT + evaluations.Count;

                PdfPTable table = new PdfPTable(columns);
                table.WidthPercentage = 100f;

                float[] width = new float[columns];
                width[0] = 1;
                width[1] = 10f;
                int i = 2;
                foreach (EvaluationListDto evaluation in evaluations)
                {
                    width[i] = 2;
                    i++;
                }

                table.SetWidths(width);

                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("APELLIDOS Y NOMBRES", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);

                foreach (EvaluationGroupDto evaluation in result)
                {
                    cell = new PdfPCell(new Paragraph(evaluation.name.ToUpper(), new Font(bf, 8, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = evaluation.colspan;
                    FormatPDF.setFormatHeaderCell(cell);
                    table.AddCell(cell);
                }

                //PdfPCell cell = new PdfPCell();
                //cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 12, 0, BaseColor.White)));
                //FormatPDF.setFormatHeaderCell(cell);
                //table.AddCell(cell);
                //cell = new PdfPCell(new Paragraph("Apellidos y Nombres", new Font(bf, 12, 0, BaseColor.White)));
                //FormatPDF.setFormatHeaderCell(cell);
                //table.AddCell(cell);

                foreach (EvaluationListDto evaluation in evaluations)
                {
                    cell = new PdfPCell(new Paragraph(evaluation.name.ToUpper(), new Font(bf, 10, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    FormatPDF.setFormatHeaderCell(cell);
                    cell.Rotation = 90;
                    table.AddCell(cell);
                }

                int index = 0;
                foreach (EnrollmentDetailListDto student in students)
                {

                    cell = new PdfPCell(new Paragraph( (index + 1).ToString() , new Font(bf, 9, Font.NORMAL, BaseColor.Black)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph( student.student_firstName + " " + student.student_lastName + " " + student.student_name , new Font(bf, 11, Font.NORMAL, BaseColor.Black)));
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    foreach (NoteListDto note in student.notes)
                    {
                        if (note.isAverage)
                         cell = new PdfPCell(new Paragraph(note.note, new Font(bf, 9, Font.BOLD, BaseColor.Black)));
                        else
                         cell = new PdfPCell(new Paragraph(note.note, new Font(bf, 9, Font.NORMAL, BaseColor.Black)));

                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        FormatPDF.setFormatItemCell(cell);
                        table.AddCell(cell);
                    }

                    index++;
                }

                document.Add(table);

                document.Close();

                byte[] byteStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MemoryStream GetStudentsByCourseWithoutNotesPDF(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID, int teacherTypeID)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
                List<EnrollmentDetailListDto> students = GetStudentsWithoutNotes(programmingID, schoolID, active, evaluationFormulaID, teacherTypeID);

                List<EvaluationListDto> evaluations = new List<EvaluationListDto>();

                EvaluationApplicationService evaluationApplicationService = new EvaluationApplicationService(this.evaluationRepository, this.evaluationExpirationRepository);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = evaluationApplicationService.GetEvaluationsHeaderFormated(evaluationFormulaID, schoolID, active, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;
                evaluations = returnListDto.evaluations;

                ProgrammingDto programing = new ProgrammingDto();
                programing = this.programmingRepository.ObtainPDF(programmingID);

                string title = "REGISTRO DE NOTAS";
                string schoolName = "";
                string subTitle = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";

                if (programing != null)
                {
                    schoolName = programing.school_name;
                    subTitle = programing.career_name;
                    header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                    header2 = "Curso: " + programing.course_name;
                    header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
                }

                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.Letter, 30F, 20f, 170f, 40f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);
                pw.PageEvent = new HeadeFooter(title, subTitle, schoolName, header1, header2, header3);

                document.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
                Font fontText = new Font(bf, 10, 0, BaseColor.Black);

                const int INIT = 2;
                int columns = 0;
                columns = INIT + evaluations.Count;

                float[] width = new float[columns];
                width[0] = 1;
                width[1] = 8f;
                int i = 2;

                foreach (EvaluationListDto evaluation in evaluations)
                {
                    width[i] = 1;
                    i++;
                }

                PdfPTable table = new PdfPTable(columns);
                table.WidthPercentage = 100f;

                table.SetWidths(width);

                table.DefaultCell.Border = 0;// Rectangle.BOX;
                table.DefaultCell.BorderColor = iTextSharp.text.html.WebColors.GetRgbColor("#FFFFFF");

                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("APELLIDOS Y NOMBRES", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);

                foreach (EvaluationGroupDto evaluation in result)
                {
                    cell = new PdfPCell(new Paragraph(evaluation.name.ToUpper() , new Font(bf, 9, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = evaluation.colspan;
                    FormatPDF.setFormatHeaderCell(cell);
                    table.AddCell(cell);
                }

                //cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 12, 0, BaseColor.White)));
                //FormatPDF.setFormatHeaderCell(cell);
                //table.AddCell(cell);
                //cell = new PdfPCell(new Paragraph("Apellidos y Nombres", new Font(bf, 12, 0, BaseColor.White)));
                //FormatPDF.setFormatHeaderCell(cell);
                //table.AddCell(cell);

                foreach (EvaluationListDto evaluation in evaluations)
                {
                    cell = new PdfPCell(new Paragraph(evaluation.name.ToUpper(), new Font(bf, 10, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Rotation = 90;
                    FormatPDF.setFormatHeaderCell(cell);
                    table.AddCell(cell);
                }

                int index = 0;
                foreach (EnrollmentDetailListDto student in students)
                {

                    cell = new PdfPCell(new Paragraph((index + 1).ToString(), new Font(bf, 9, Font.NORMAL, BaseColor.Black)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(student.student_firstName + " " + student.student_lastName + " " + student.student_name, new Font(bf, 11, Font.NORMAL, BaseColor.Black)));
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    foreach (EvaluationListDto evaluation in evaluations)
                    {
                        cell = new PdfPCell(new Paragraph("", new Font(bf, 12, Font.NORMAL, BaseColor.Black)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        FormatPDF.setFormatItemCell(cell);
                        table.AddCell(cell);
                    }

                    index++;
                }

                document.Add(table);

                document.Close();

                byte[] byteStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MemoryStream GetStudentsByCourseWithoutNotesXLS(Int32 programmingID, Int32 schoolID, Boolean active, Int32 evaluationFormulaID,int teacherTypeID)
        {
            //try
            //{
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
            List<EnrollmentDetailListDto> students = GetStudentsWithoutNotes(programmingID, schoolID, active, evaluationFormulaID,teacherTypeID);

            List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
                //evaluations = evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active, teacherTypeID);

                EvaluationApplicationService evaluationApplicationService = new EvaluationApplicationService(this.evaluationRepository, this.evaluationExpirationRepository);

                ReturnListDto returnListDto = new ReturnListDto();
                returnListDto = evaluationApplicationService.GetEvaluationsHeaderFormated(evaluationFormulaID, schoolID, active, teacherTypeID);

                List<EvaluationGroupDto> result = new List<EvaluationGroupDto>();
                result = returnListDto.result;
                evaluations = returnListDto.evaluations;

                List<EvaluationListDto> legends = this.evaluationRepository.GetEvaluationsLegend(evaluationFormulaID, schoolID, active);

            ProgrammingDto programing = new ProgrammingDto();
            programing = this.programmingRepository.ObtainPDF(programmingID);

            string title = "REGISTRO DE NOTAS";
            string schoolName = "";
            string subTitle = "";
            string header1 = "";
            string header2 = "";
            string header3 = "";

            if (programing != null)
            {
                schoolName = programing.school_name;
                subTitle = programing.career_name;
                header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                header2 = "Curso: " + programing.course_name;
                header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
            }

            var ms = new MemoryStream();

                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Registro de notas");

                IRow row = excelSheet.CreateRow(0);
                ICell cell = row.CreateCell(1);
                cell.SetCellValue(title);
                FormatExcel.setFormatTitleCellBold(workbook, cell);

                row = excelSheet.CreateRow(1);
                cell = row.CreateCell(1);
                cell.SetCellValue(subTitle);
                FormatExcel.setFormatSubTitleCell(workbook, cell);

                row = excelSheet.CreateRow(3);
                cell = row.CreateCell(1);
                cell.SetCellValue(header1);
                FormatExcel.setFormatHeaderCellBold(workbook, cell);

                row = excelSheet.CreateRow(4);
                cell = row.CreateCell(1);
                cell.SetCellValue(header2 + " - " + header3);
                FormatExcel.setFormatHeaderCell(workbook, cell);


                row = excelSheet.CreateRow(6);
                cell = row.CreateCell(0);
                cell.SetCellValue("N°");
                FormatExcel.setFormatColumnCell(workbook, cell);

                cell = row.CreateCell(1);
                cell.SetCellValue("APELLIDOS Y NOMBRES");
                FormatExcel.setFormatColumnCell(workbook, cell);

                excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 7, 0, 0));
                excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 7, 1, 1));

                int jj = 2;
                foreach (EvaluationGroupDto evaluation in result)
                {
                    cell = row.CreateCell(jj);
                    cell.SetCellValue(evaluation.name.ToUpper());
                    FormatExcel.setFormatColumnCell(workbook, cell);

                CellRangeAddress mergedCell = new NPOI.SS.Util.CellRangeAddress(6, 6, jj, jj + evaluation.colspan - 1);
                excelSheet.AddMergedRegion(mergedCell);

                ICellStyle CellStyle = workbook.CreateCellStyle();
                RegionUtil.SetBorderTop(1, mergedCell, excelSheet, workbook);
                RegionUtil.SetBorderBottom(1, mergedCell, excelSheet, workbook);
                RegionUtil.SetBorderLeft(1, mergedCell, excelSheet, workbook);
                RegionUtil.SetBorderRight(1, mergedCell, excelSheet, workbook);

                jj = jj + evaluation.colspan;
                }

   
            row = excelSheet.CreateRow(7);
             
                excelSheet.SetColumnWidth(1, 10000);

                int j = 2;
                foreach (EvaluationListDto evaluation in evaluations)
                {
                    cell = row.CreateCell(j);
                    cell.SetCellValue(evaluation.name.ToUpper());
                    FormatExcel.setFormatColumnCellVertical(workbook, cell);

                    j++;
                }

            int index = 7;
               
                foreach (EnrollmentDetailListDto student in students)
            {

                    row = excelSheet.CreateRow(index+1);

                    cell = row.CreateCell(0);
                    cell.SetCellValue(student.orderNumber);
                    FormatExcel.setFormatItemCell(workbook, cell);

                    cell = row.CreateCell(1);
                    cell.SetCellValue(student.student_firstName + " " + student.student_lastName + " " + student.student_name);
                    FormatExcel.setFormatItemCell(workbook, cell);

                    int i = 2;
                    foreach (EvaluationListDto evaluation in evaluations)
                {
              
                        cell = row.CreateCell(i);
                        cell.SetCellValue("");
                        FormatExcel.setFormatItemCell(workbook, cell);
                        i++;

                }

                    index++;
            }

 

            workbook.Write(ms);
           
            byte[] byteStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return ms;
        //}
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

}



        public MemoryStream GetStudentsByCourseWithoutNotesPDF_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            try
            {
                BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
       
                List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

                ProgrammingDto programing = new ProgrammingDto();
                programing = this.programmingRepository.ObtainPDF(programmingID);

                string title = "REGISTRO DE ASISTENCIA";
                string schoolName = "";
                string subTitle = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";
                int classNumber = 0;

                if (programing != null)
                {
                    schoolName = programing.school_name;
                    subTitle = programing.career_name;
                    header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                    header2 = "Curso: " + programing.course_name;
                    header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
                    classNumber = programing.classesNumber;
                }

                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.Letter, 30F, 20f, 170f, 40f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);
                pw.PageEvent = new HeadeFooter(title, subTitle, schoolName, header1, header2, header3);

                document.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
                Font fontText = new Font(bf, 10, 0, BaseColor.Black);

                const int INIT = 2;
                int columns = 0;
                columns = INIT + classNumber;

                float[] width = new float[columns];
                width[0] = 1;
                width[1] = 8f;
                int i = 2;

                for (int j=0; j<= classNumber-1;j++)
                {
                    width[i] = 1;
                    i++;
                }

                PdfPTable table = new PdfPTable(columns);
                table.WidthPercentage = 100f;

                table.SetWidths(width);

                table.DefaultCell.Border = 0;// Rectangle.BOX;
                table.DefaultCell.BorderColor = iTextSharp.text.html.WebColors.GetRgbColor("#FFFFFF");

                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 10, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("APELLIDOS Y NOMBRES", new Font(bf, 10, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);



                for (int j = 0; j <= classNumber - 1; j++)
                {
                    cell = new PdfPCell(new Paragraph("       ", new Font(bf, 10, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.SetLeading(40f,0f);
                    cell.Rowspan = 2;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                   FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);
                }

                int index = 0;
                foreach (EnrollmentDetailListDto student in students)
                {

                    cell = new PdfPCell(new Paragraph((index + 1).ToString(), new Font(bf, 9, Font.NORMAL, BaseColor.Black)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(student.student_firstName + " " + student.student_lastName + " " + student.student_name, new Font(bf, 11, Font.NORMAL, BaseColor.Black)));
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    for (int j = 0; j <= classNumber - 1; j++)
                    {
                        cell = new PdfPCell(new Paragraph("", new Font(bf, 12, Font.NORMAL, BaseColor.Black)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        FormatPDF.setFormatItemCell(cell);
                        table.AddCell(cell);
                    }

                    index++;
                }

                document.Add(table);

                document.Close();

                byte[] byteStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MemoryStream GetStudentsByCoursePDF_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            try
            {
                List<AssistanceListDto> lessons = new List<AssistanceListDto>();
                lessons = this.assistanceRepository.GetLessons(schoolID, programmingID, active);

                List<StudentAssistanceDto> students = GetStudentsAssistance(programmingID, schoolID, active);

                ProgrammingDto programing = new ProgrammingDto();
                programing = this.programmingRepository.ObtainPDF(programmingID);

                string title = "REPORTE DE ASISTENCIA";
                string schoolName = "";
                string subTitle = "";
                string header1 = "";
                string header2 = "";
                string header3 = "";

                if (programing != null)
                {
                    schoolName = programing.school_name;
                    subTitle = programing.career_name;
                    header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                    header2 = "Curso: " + programing.course_name;
                    header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
                }

                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.Letter, 30F, 20f, 170f, 40f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);
                pw.PageEvent = new HeadeFooter(title, subTitle, schoolName, header1, header2, header3);

                document.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
                Font fontText = new Font(bf, 10, 0, BaseColor.Black);

                const int INIT = 2;
                int columns = 0;
                columns = INIT + lessons.Count;

                PdfPTable table = new PdfPTable(columns);
                table.WidthPercentage = 100f;

                float[] width = new float[columns];
                width[0] = 1;
                width[1] = 10f;
                int i = 2;
                foreach (AssistanceListDto lesson in lessons)
                {
                    width[i] = 1;
                    i++;
                }

                table.SetWidths(width);

                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Paragraph(" N°", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("APELLIDOS Y NOMBRES", new Font(bf, 12, 0, BaseColor.White)));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                FormatPDF.setFormatHeaderCell(cell);
                table.AddCell(cell);

             
                    cell = new PdfPCell(new Paragraph("DÍAS", new Font(bf, 9, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = lessons.Count;
                    FormatPDF.setFormatHeaderCell(cell);
                    table.AddCell(cell);
                

                foreach (AssistanceListDto lesson in lessons)
                {
                    cell = new PdfPCell(new Paragraph(lesson.dateClassAgo.ToUpper(), new Font(bf, 9, 0, BaseColor.White)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    FormatPDF.setFormatHeaderCell(cell);
                    cell.Rotation = 90;
                    table.AddCell(cell);
                }

                List<AssistanceDetailDto> assistances = new List<AssistanceDetailDto>();
                assistances = this.assistanceDetailRepository.GetByprogrammingIDByschoolIDByactive(programmingID, schoolID, active);

                List<AssistanceTypeListDto> assistanceTypes = new List<AssistanceTypeListDto>();
                assistanceTypes = this.assistanceTypeRepository.GetByschoolIDByactive(schoolID, active);

                AssistanceTypeListDto isLackAssistance = new AssistanceTypeListDto();
                isLackAssistance = assistanceTypes.Where(e => e.isLack).FirstOrDefault();


                int index = 0;
                foreach (StudentAssistanceDto student in students)
                {

                    cell = new PdfPCell(new Paragraph((index + 1).ToString(), new Font(bf, 9, Font.NORMAL, BaseColor.Black)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(student.student_firstName + " " + student.student_lastName + " " + student.student_name, new Font(bf, 11, Font.NORMAL, BaseColor.Black)));
                    FormatPDF.setFormatItemCell(cell);
                    table.AddCell(cell);


                    foreach (AssistanceListDto lesson in lessons)
                    {

                        int assistanceDetailID = 0;
                        string typeAssistance = "";
                        int assistanceTypeID = 0;

                        AssistanceDetailDto obj = new AssistanceDetailDto();
                        obj = assistances.Where(e => e.studentID == student.studentID && e.assistanceID == lesson.assistanceID).FirstOrDefault();

                        if (obj != null)
                        {
                            assistanceDetailID = obj.assistanceDetailID;
                            typeAssistance = obj.assistanceType_abbreviation;
                            assistanceTypeID = obj.assistanceTypeID;
                        }
                        else
                        {
                            if (isLackAssistance != null)
                            {
                                typeAssistance = isLackAssistance.abbreviation;
                                assistanceTypeID = isLackAssistance.assistanceTypeID;
                            }
                        }

                        cell = new PdfPCell(new Paragraph(typeAssistance, new Font(bf, 9, Font.NORMAL, BaseColor.Black)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        FormatPDF.setFormatItemCell(cell);
                        table.AddCell(cell);

                    }


                    index++;
                }

                document.Add(table);

                document.Close();

                byte[] byteStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MemoryStream GetStudentsByCourseWithoutNotesXLS_Assitance(Int32 programmingID, Int32 schoolID, Boolean active)
        {
            //try
            //{
            BaseResponseDto<EnrollmentDetailListDto> baseResponseDto = new BaseResponseDto<EnrollmentDetailListDto>();
            List<EnrollmentDetailListDto> students = this.enrollmentDetailRepository.GetStudentsByCourse(programmingID, schoolID, active);

            List<EvaluationListDto> evaluations = new List<EvaluationListDto>();
            //evaluations = evaluationRepository.GetEvaluations(evaluationFormulaID, schoolID, active, teacherTypeID);
            List<AssistanceTypeListDto> legends = this.assistanceTypeRepository.GetByschoolIDByactive(schoolID, active);
            ProgrammingDto programing = new ProgrammingDto();
            programing = this.programmingRepository.ObtainPDF(programmingID);

            string title = "REGISTRO DE ASISTENCIA";
            string schoolName = "";
            string subTitle = "";
            string header1 = "";
            string header2 = "";
            string header3 = "";
            int classesNumber = 0;

            if (programing != null)
            {
                schoolName = programing.school_name;
                subTitle = programing.career_name;
                header1 = "Docente: " + programing.teacher_name + " " + programing.teacher_firstName + " " + programing.teacher_lastName;
                header2 = "Curso: " + programing.course_name;
                header3 = "Turno: " + programing.turn_name + " - Sección: " + programing.grade + programing.section_name;
                classesNumber = programing.classesNumber;
            }

            var ms = new MemoryStream();

            IWorkbook workbook;
            workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Registro de asistencia");

            IRow row = excelSheet.CreateRow(0);
            ICell cell = row.CreateCell(1);
            cell.SetCellValue(title);
            FormatExcel.setFormatTitleCellBold(workbook, cell);

            row = excelSheet.CreateRow(1);
            cell = row.CreateCell(1);
            cell.SetCellValue(subTitle);
            FormatExcel.setFormatSubTitleCell(workbook, cell);

            row = excelSheet.CreateRow(3);
            cell = row.CreateCell(1);
            cell.SetCellValue(header1);
            FormatExcel.setFormatHeaderCellBold(workbook, cell);

            row = excelSheet.CreateRow(4);
            cell = row.CreateCell(1);
            cell.SetCellValue(header2 + " - " + header3);
            FormatExcel.setFormatHeaderCell(workbook, cell);


            row = excelSheet.CreateRow(6);
            cell = row.CreateCell(0);
            cell.SetCellValue("N°");
            FormatExcel.setFormatColumnCell(workbook, cell);

            cell = row.CreateCell(1);
            cell.SetCellValue("APELLIDOS Y NOMBRES");
            FormatExcel.setFormatColumnCell(workbook, cell);

            excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 7, 0, 0));
            excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 7, 1, 1));

            int jj = 2;

            cell = row.CreateCell(jj);
            cell.SetCellValue("DÍAS");
            FormatExcel.setFormatColumnCell(workbook, cell);

            CellRangeAddress mergedCell = new NPOI.SS.Util.CellRangeAddress(6, 6, jj, classesNumber);
            excelSheet.AddMergedRegion(mergedCell);

            ICellStyle CellStyle = workbook.CreateCellStyle();
            RegionUtil.SetBorderTop(1, mergedCell, excelSheet, workbook);
            RegionUtil.SetBorderBottom(1, mergedCell, excelSheet, workbook);
            RegionUtil.SetBorderLeft(1, mergedCell, excelSheet, workbook);
            RegionUtil.SetBorderRight(1, mergedCell, excelSheet, workbook);

            row = excelSheet.CreateRow(7);

            excelSheet.SetColumnWidth(1, 10000);

            int j = 2;
            for (int i=0; i<classesNumber-1;i++)
            {
                cell = row.CreateCell(j);
                cell.SetCellValue("");
                FormatExcel.setFormatColumnCellVertical(workbook, cell);

                j++;
            }

            int index = 7;

            foreach (EnrollmentDetailListDto student in students)
            {

                row = excelSheet.CreateRow(index + 1);

                cell = row.CreateCell(0);
                cell.SetCellValue(student.orderNumber);
                FormatExcel.setFormatItemCell(workbook, cell);

                cell = row.CreateCell(1);
                cell.SetCellValue(student.student_firstName + " " + student.student_lastName + " " + student.student_name);
                FormatExcel.setFormatItemCell(workbook, cell);

                int i = 2;
                for (int k = 0; k < classesNumber - 1;k++)
                {
                    cell = row.CreateCell(i);
                    cell.SetCellValue("");
                    FormatExcel.setFormatItemCell(workbook, cell);
                    i++;
                }

                index++;
            }


            row = excelSheet.CreateRow(index + 2);
            cell = row.CreateCell(1);
            cell.SetCellValue("Leyenda:");
            FormatExcel.setFormatFooterCellBold(workbook, cell);

            foreach (AssistanceTypeListDto legend in legends)
            {
                row = excelSheet.CreateRow(index + 3);
                cell = row.CreateCell(1);
                cell.SetCellValue(legend.abbreviation.ToUpper() + ":" +legend.name);
                index++;
            }


            workbook.Write(ms);

            byte[] byteStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return ms;
            //}
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }

        }

    }
}
