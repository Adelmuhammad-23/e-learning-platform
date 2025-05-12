using e_learning.Data.Helpers;

namespace e_learning.Services.Abstructs
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInCourseAsync(int studentId, int courseId);
        public Task<bool> isEnrollment(int studentId, int courseId);

        Task<IEnumerable<EnrollmentDTO>> GetEnrollmentsForStudentAsync(int studentId);
    }

}
