using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface IReviewService
    {
        Task<Reviews> GetByIdAsync(int reviewId);
        Task<bool> DeleteAsync(int reviewId);
        Task<bool> UpdateAsync(Reviews review);


        Task<List<Reviews>> GetReviewsByCourseAsync(int courseId);
        Task<string> AddReviewAsync(Reviews dto);
    }


}
