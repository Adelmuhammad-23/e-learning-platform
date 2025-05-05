using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;

namespace e_learning.Services.Implementations
{
    public class QuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Task<List<Question>> GetAllAsync() => _questionRepository.GetAllAsync();
        public Task<Question> GetByIdAsync(int id) => _questionRepository.GetByIdAsync(id);
        public Task AddAsync(Question question) => _questionRepository.AddAsync(question);
        public Task UpdateAsync(Question question) => _questionRepository.UpdateAsync(question);
        public Task DeleteAsync(int id) => _questionRepository.DeleteAsync(id);
    }
}
