using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllAsync() => await _context.Questions.Include(q => q.Choices).ToListAsync();
        public async Task<Question> GetByIdAsync(int id) => await _context.Questions.Include(q => q.Choices).FirstOrDefaultAsync(q => q.Id == id);
        public async Task AddAsync(Question question) { _context.Questions.Add(question); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Question question) { _context.Questions.Update(question); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var question = await _context.Questions.FindAsync(id); if (question != null) { _context.Questions.Remove(question); await _context.SaveChangesAsync(); } }
    }
}
