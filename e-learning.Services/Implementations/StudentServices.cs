using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Implementations
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;


        public StudentServices(IStudentRepository studentRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
            _mapper = mapper;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var stu = await _studentRepository.AddStudentAsync(student);
            if (stu != null)
                return stu;
            return "Can't add student because is null !";
        }

        public async Task<string> DeleteStudentAsync(int studentId)
        {
            var student = await _studentRepository.GetStudentAsync(studentId);
            if (student == null)
                return "NotFound";
            var studentRemoved = await _studentRepository.DeleteStudentAsync(student);
            return studentRemoved;
        }

        public Task<IActionResult> GetAllStudent()
        {
            throw new NotImplementedException();
        }

        public async Task<List<StudentDTO>> GetAllStudentAsync()
        {
            var students = await _studentRepository.GetAllStudentAsync();
            var studentsMapping = _mapper.Map<List<StudentDTO>>(students);
            return studentsMapping;
        }

        public async Task<StudentDTO> GetStudentAsync(int id)
        {
            var stu = await _studentRepository.GetStudentAsync(id);
            if (stu != null)
            {
                var stuMapping = _mapper.Map<StudentDTO>(stu);
                return stuMapping;
            }
            return null;
        }

        public async Task<string> UpdateStudentAsync(int id, UpdateStudentDTO updateStudent, IFormFile ImageUrl)
        {


            var existing = await _studentRepository.GetStudentAsync(id);
            if (existing == null) return "NotFound";

            var studentMapping = _mapper.Map(updateStudent, existing);

            var stu = await _studentRepository.UpdateStudentAsync(studentMapping);
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return "error in httpContext";

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images", "students");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(ImageUrl.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUrl.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/images/students/{uniqueFileName}";

            studentMapping.Image = fileUrl;

            existing.Name = studentMapping.Name;
            existing.Email = studentMapping.Email;
            existing.Image = studentMapping.Image;

            await _studentRepository.UpdateStudentAsync(existing);
            return "updated";
        }
    }
}
