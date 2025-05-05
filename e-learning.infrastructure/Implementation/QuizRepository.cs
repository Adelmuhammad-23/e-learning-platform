﻿using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetAllAsync() => await _context.Quizzes.Include(q => q.Questions).ThenInclude(c => c.Choices).ToListAsync();
        public async Task<Quiz> GetByIdAsync(int id) => await _context.Quizzes.Include(q => q.Questions).ThenInclude(c => c.Choices).FirstOrDefaultAsync(q => q.Id == id);
        public async Task AddAsync(Quiz quiz) { _context.Quizzes.Add(quiz); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Quiz quiz) { _context.Quizzes.Update(quiz); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var quiz = await _context.Quizzes.FindAsync(id); if (quiz != null) { _context.Quizzes.Remove(quiz); await _context.SaveChangesAsync(); } }
    }
}
