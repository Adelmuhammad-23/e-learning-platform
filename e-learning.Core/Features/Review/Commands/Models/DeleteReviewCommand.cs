using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Review.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Responses<string>>
    {
        public int ReviewId { get; set; }

        public DeleteReviewCommand(int reviewId)
        {
            ReviewId = reviewId;
        }
    }
}