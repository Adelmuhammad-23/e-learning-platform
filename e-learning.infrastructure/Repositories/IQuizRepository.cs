using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllAsync();
        Task<double> GetScore(int studentId, int quizId);
        Task<Quiz> GetByTitleAsync(string title);

        Task<Quiz> GetByIdAsync(int id);
        Task AddAsync(Quiz quiz);
        public Task<StudentQuiz> GetStudentQuizAsync(int studentId, int quizId);

        public Task AddStudentQuizAsync(StudentQuiz studentQuiz);

        public Task SaveChangesAsync();
        Task UpdateAsync(Quiz quiz);
        Task DeleteAsync(int id);
    }
}
