using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class AdminServices : IAdminServices
    {


        private readonly IInstructorService _instructorService;
        private readonly IAdminRepository _adminRepository;

        public AdminServices(IInstructorService instructorService, IAdminRepository adminRepository)
        {

            _instructorService = instructorService;
            _adminRepository = adminRepository;
        }
        public async Task<bool> Approved(Instructor instructor)
        {
            var approved = await _adminRepository.Approved(instructor);
            return approved;
        }
    }
}
