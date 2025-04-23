using e_learning.Core.Bases;
using e_learning.Core.Features.Review.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Review.Queries.Models
{
    public class GetReviewByIdQuery : IRequest<Responses<GetReviewByIdResponse>>
    {
        public int ReviewId { get; set; }

        public GetReviewByIdQuery(int reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
