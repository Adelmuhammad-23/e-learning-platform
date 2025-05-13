using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;

namespace e_learning.Services.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IInstructorRepository _instructorRepository;

        public AdminRepository(IInstructorRepository instructorRepository, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _instructorRepository = instructorRepository;
        }

        public async Task<bool> Approved(Instructor instructor)
        {
            instructor.isApproved = true;
            _dbContext.instructors.Update(instructor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
