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

        public async Task<string> AddReviewAsync(Reviews review)
        {
            var added = await _repository.AddReviewAsync(review);
            return "Success";
        }
    }

}
