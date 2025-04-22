using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IReviewRepository
    {
        Task<Reviews> GetByIdAsync(int reviewId);
        Task<bool> DeleteAsync(int reviewId);
        Task<bool> UpdateAsync(Reviews review);


        Task<List<Reviews>> GetCourseReviewsAsync(int courseId);
        Task<string> AddReviewAsync(Reviews review);

    }

}
