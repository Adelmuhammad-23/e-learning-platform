using e_learning.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace e_learning.Core.Features.Courses.Commands.Models
{
    public class AddCourseCommand : IRequest<Responses<string>>
    {
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorEmail { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; } = 0;
        public int CategoryId { get; set; }
    }
}
