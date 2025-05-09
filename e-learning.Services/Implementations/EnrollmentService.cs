using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
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

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsForStudentAsync(int studentId)
        {
            return await _enrollmentRepository.GetEnrollmentsByStudentAsync(studentId);
        }
    }

}
