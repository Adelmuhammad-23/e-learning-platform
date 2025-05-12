using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Review.Commands.Models
{
    public class UpdateReviewCommand : IRequest<Responses<string>>
    {
        public UpdateReviewCommand(int reviewId)
        {
            ReviewId = reviewId;
        }

        public int ReviewId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

    }
}