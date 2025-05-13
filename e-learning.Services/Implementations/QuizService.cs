using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Implementations
{
    public class QuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseServices _courseServices;
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IInstructorService _instructorService;
        public QuizService(IEnrollmentService enrollmentService, IHttpContextAccessor httpContextAccessor, IQuizRepository quizRepository, IModuleService moduleService, ICourseServices courseServices, IInstructorService instructorService, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _courseServices = courseServices;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentService = enrollmentService;
            _moduleService = moduleService;
            _instructorService = instructorService;


        }
        public async Task SubmitQuizScoreAsync(int studentId, int quizId, double score)
        {

            var entry = await _quizRepository.GetStudentQuizAsync(studentId, quizId);
            if (entry == null)
            {
                await _quizRepository.AddStudentQuizAsync(new StudentQuiz
                {
                    StudentId = studentId,
                    QuizId = quizId,
                    Score = score
                });
            }
            else
            {
                entry.Score = score;
            }

            await _quizRepository.SaveChangesAsync();
        }
        public async Task<List<CreateQuizDto>> GetAllAsync()
        {

            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            var studentId = int.Parse(userIdStr ?? "-1");

            var quizzes = await _quizRepository.GetAllAsync();
            var quizMapping = _mapper.Map<List<CreateQuizDto>>(quizzes);

            foreach (var quiz in quizMapping)
            {
                var checkStudentCourseEnrolled = await _enrollmentService.isEnrollment(studentId, quiz.CourseId);

                if (!checkStudentCourseEnrolled)
                {
                    quiz.Questions.Clear();
                    quiz.Message = "You are not registered in this course.";
                }
                else
                    quiz.Message = "You can start solving the quiz.";

            }

            foreach (var quizDto in quizMapping)
            {
                var quize = await _quizRepository.GetByTitleAsync(quizDto.Title);
                var score = await _quizRepository.GetScore(studentId, quize.Id);
                quizDto.Score = score;
            }
            return quizMapping;
        }
        private async Task GetScoreAsync(List<CreateQuizDto> createQuizDtos, int studentId, int quizId)
        {
            foreach (var quizDto in createQuizDtos)
            {
                var quize = await _quizRepository.GetByTitleAsync(quizDto.Title);
                var score = await _quizRepository.GetScore(studentId, quize.Id);
                quizDto.Score = score;
            }
        }
        public async Task<CreateQuizDto> GetByIdAsync(int id)
        {
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            var userIdStr = (role == "Instructor") ?
                _httpContextAccessor.HttpContext?.User?.FindFirst("instructorId")?.Value :
                _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            var userId = int.Parse(userIdStr ?? "-1");
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                return null;
            var quizMapping = _mapper.Map<CreateQuizDto>(quiz);
            if (role == "Instructor")
            {
                var CheckInstructorCourse = await _instructorService.isInstrucorCourse(userId, quiz.CourseId);
                if (!CheckInstructorCourse)
                {
                    quiz.Questions.Clear();
                    quizMapping.Message = "You are not registered in this course.";
                }
                else
                    quizMapping.Message = "success";
            }
            else
            {
                var checkStudentCourseEnrolled = await _enrollmentService.isEnrollment(userId, quiz.CourseId);
                if (!checkStudentCourseEnrolled)
                {
                    quiz.Questions.Clear();
                    quizMapping.Message = "You are not registered in this course.";
                }
                else
                    quizMapping.Message = "You can start solving the quiz.";
            }

            var getQuiz = await _quizRepository.GetByTitleAsync(quizMapping.Title);
            var score = await _quizRepository.GetScore(userId, getQuiz.Id);
            quizMapping.Score = score;


            return quizMapping;
        }
        public async Task<string> AddAsync(CreateQuizDto quiz)
        {
            var existingCourse = await _courseServices.GetCourseByIdAsync(quiz.CourseId);
            if (existingCourse == null)
                return ("Course not found");
            var existingModule = await _moduleService.GetModuleByIdAsync(quiz.ModuleId);
            if (existingCourse == null)
                return ("Module not found");

            var mappedQuiz = _mapper.Map<Quiz>(quiz);
            await _quizRepository.AddAsync(mappedQuiz);
            return ("Course Added is successfully");

        }

        public async Task<string> UpdateAsync(int id, CreateQuizDto quizDto)
        {
            var existingQuiz = await _quizRepository.GetByIdAsync(id);
            if (existingQuiz == null)
                return ("Quiz not found");

            var existingCourse = await _courseServices.GetCourseByIdAsync(quizDto.CourseId);
            if (existingCourse == null)
                return ("Course not found");

            var existingModule = await _moduleService.GetModuleByIdAsync(quizDto.ModuleId);
            if (existingCourse == null)
                return ("Module not found");

            var updatedQuiz = _mapper.Map(quizDto, existingQuiz);

            await _quizRepository.UpdateAsync(updatedQuiz);
            return ("Course updated is successfully");
        }

        public Task DeleteAsync(int id) => _quizRepository.DeleteAsync(id);
    }
}
