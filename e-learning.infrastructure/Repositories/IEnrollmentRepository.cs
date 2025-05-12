using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> GetEnrollmentAsync(int studentId, int courseId);
        Task<bool> isEnrollment(int studentId, int courseId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId);
        Task AddEnrollmentAsync(Enrollment enrollment);
    }

}
