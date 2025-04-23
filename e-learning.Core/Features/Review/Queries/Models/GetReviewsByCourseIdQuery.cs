using e_learning.Core.Bases;
using e_learning.Core.Features.Review.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Review.Queries.Models
{
    public class GetReviewsByCourseIdQuery : IRequest<Responses<List<GetReviewsByCourseIdResponse>>>
    {
        public int CourseId { get; set; }

        public GetReviewsByCourseIdQuery(int courseId)
        {
            CourseId = courseId;
        }
    }
}
