using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInCourseAsync(int studentId, int courseId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsForStudentAsync(int studentId);
    }

}
