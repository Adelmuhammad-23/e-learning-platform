using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface IAdminServices
    {
        public Task<bool> Approved(Instructor instructor);

    }
}
