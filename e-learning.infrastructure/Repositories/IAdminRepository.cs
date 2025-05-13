using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IAdminRepository
    {
        public Task<bool> Approved(Instructor instructor);
    }
}
