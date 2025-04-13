using e_learning.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace e_learning.Core.Features.Videos.Commands.Models
{
    public class AddVideoInCourse : IRequest<Responses<string>>
    {
        public string Title { get; set; }
        public IFormFile videoFile { get; set; }
        public int CourseId { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
