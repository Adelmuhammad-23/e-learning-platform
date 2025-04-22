using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface IReviewService
    {
        Task<List<Reviews>> GetReviewsByCourseAsync(int courseId);
        Task<string> AddReviewAsync(Reviews dto);
    }


}
