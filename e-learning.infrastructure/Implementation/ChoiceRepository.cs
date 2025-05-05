using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ChoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Choice>> GetAllAsync() => await _context.Choices.ToListAsync();
        public async Task<Choice> GetByIdAsync(int id) => await _context.Choices.FindAsync(id);
        public async Task AddAsync(Choice choice) { _context.Choices.Add(choice); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Choice choice) { _context.Choices.Update(choice); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var choice = await _context.Choices.FindAsync(id); if (choice != null) { _context.Choices.Remove(choice); await _context.SaveChangesAsync(); } }
    }

}
