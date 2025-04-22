using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Reviews>> GetReviewsByCourseAsync(int courseId)
        {
            var reviews = await _repository.GetCourseReviewsAsync(courseId);
            return reviews;
        }
        public async Task<Reviews> GetByIdAsync(int reviewId)
        {
            return await _repository.GetByIdAsync(reviewId);
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            return await _repository.DeleteAsync(reviewId);
        }
        public async Task<bool> UpdateAsync(Reviews review)
        {
            return await _repository.UpdateAsync(review);
        }


        public async Task<string> AddReviewAsync(Reviews review)
        {
            var added = await _repository.AddReviewAsync(review);
            return "Success";
        }
    }

}
