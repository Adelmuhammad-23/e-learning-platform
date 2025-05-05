using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class QuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICourseServices _courseServices;
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, IModuleService moduleService, ICourseServices courseServices, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _courseServices = courseServices;
            _mapper = mapper;
            _moduleService = moduleService;
        }

        public async Task<List<QuizWithQuestionsDto>> GetAllAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return _mapper.Map<List<QuizWithQuestionsDto>>(quizzes);
        }

        public async Task<QuizWithQuestionsDto> GetByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            return _mapper.Map<QuizWithQuestionsDto>(quiz);
        }
        public async Task<string> AddAsync(CreateQuizDto quiz)
        {
            var existingCourse = await _courseServices.GetCourseByIdAsync(quiz.CourseId);
            if (existingCourse == null)
                return ("Course not found");
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
