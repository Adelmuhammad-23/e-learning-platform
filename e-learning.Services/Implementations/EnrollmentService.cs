using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task EnrollStudentInCourseAsync(int studentId, int courseId)
        {
            var existingEnrollment = await _enrollmentRepository.GetEnrollmentAsync(studentId, courseId);
            if (existingEnrollment != null)
                throw new Exception("Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow
            };

            await _enrollmentRepository.AddEnrollmentAsync(enrollment);
        }

        public async Task<IEnumerable<EnrollmentDTO>> GetEnrollmentsForStudentAsync(int studentId)
        {
            var enrollmentsForStudent = await _enrollmentRepository.GetEnrollmentsByStudentAsync(studentId);
            if (enrollmentsForStudent == null)
                return null;
            var enrollmentsForStudentMapping = _mapper.Map<IEnumerable<EnrollmentDTO>>(enrollmentsForStudent);
            return enrollmentsForStudentMapping;
        }

        public async Task<bool> isEnrollment(int studentId, int courseId)
        {
            var result = await _enrollmentRepository.isEnrollment(studentId, courseId);
            return result;
        }
    }

}
