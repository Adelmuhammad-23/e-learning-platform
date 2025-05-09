using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Review.Commands.Models
{
    public class AddReviewCommand : IRequest<Responses<string>>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
