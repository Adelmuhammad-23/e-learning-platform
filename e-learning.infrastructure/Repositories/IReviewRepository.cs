using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Reviews>> GetCourseReviewsAsync(int courseId);
        Task<string> AddReviewAsync(Reviews review);

    }

}
