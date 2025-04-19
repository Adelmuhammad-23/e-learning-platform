using e_learning.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace e_learning.Core.Features.Instructors.Commands.Models
{
    public class UpdateInstructorCommand : IRequest<Responses<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
}
