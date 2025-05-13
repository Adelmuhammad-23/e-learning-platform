using e_learning.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace e_learning.Core.Features.Instructors.Commands.Models
{
    public class AddProfessionalInstructorCommand : IRequest<Responses<string>>
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public List<IFormFile> Certificates { get; set; }
    }
}
