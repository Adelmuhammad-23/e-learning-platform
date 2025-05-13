using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Implementations
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;
        private readonly ICourseRepository _CourseRespository;
        private readonly IMapper _mapper;
        public InstructorService(IMapper mapper, IInstructorRepository repository,
             IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost,
             ICourseRepository CourseRespository
             )
        {
            _mapper = mapper;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
            _CourseRespository = CourseRespository;
        }
        public async Task AddInstructorAsync(Instructor instructor)
        {
            await _repository.AddAsync(instructor);
        }
        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Instructor?> GetInstructorByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Instructor?> GetInstructorByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<bool> UpdateInstructorAsync(int id, Instructor updatedInstructor, IFormFile ImageUrl)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return false;

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images", "instructors");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            if (updatedInstructor.Image != null)
            {
                var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(ImageUrl.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }

                var fileUrl = $"{request.Scheme}://{request.Host}/uploads/images/instructors/{uniqueFileName}";

                updatedInstructor.Image = fileUrl;
            }
            existing.Name = updatedInstructor.Name;
            existing.Email = updatedInstructor.Email;
            existing.Image = updatedInstructor.Image;
            existing.Bio = updatedInstructor.Bio;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _repository.GetByIdAsync(id);
            if (instructor == null) return false;

            await _repository.DeleteAsync(instructor);
            return true;
        }

        public async Task<bool> isInstrucorCourse(int InstructorId, int CourseId)
        {
            var Course = await this._CourseRespository.GetCourseByIdAsync(CourseId);
            if (Course == null) return false;
            if (Course.InstructorId != InstructorId) return false;
            return true;
        }

        public async Task<bool> AddProfessionalInstructorAsync(int id, Instructor updatedInstructor, List<IFormFile> certificates)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return false;

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "certificates", "instructors");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var certificateUrls = new List<string>();

            foreach (var certificate in certificates)
            {
                if (certificate.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 8)}{Path.GetExtension(certificate.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await certificate.CopyToAsync(stream);
                    }

                    var fileUrl = $"{request.Scheme}://{request.Host}/uploads/certificates/instructors/{uniqueFileName}";
                    certificateUrls.Add(fileUrl);
                }
            }

            existing.Certificates = certificateUrls;

            existing.Position = updatedInstructor.Position;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<List<InstructorDTO>> GetInstructorsIsNotApproved()
        {
            var instructorsNotApproved = await _repository.GetInstructorsIsNotApproved();
            var mapping = _mapper.Map<List<InstructorDTO>>(instructorsNotApproved);

            return mapping;
        }
    }
}
