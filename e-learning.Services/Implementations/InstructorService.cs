using e_learning.Data.Entities;
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

        public InstructorService(IInstructorRepository repository,
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
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

            var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(ImageUrl.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUrl.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/images/instructors/{uniqueFileName}";

            updatedInstructor.Image = fileUrl;

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
    }
}
