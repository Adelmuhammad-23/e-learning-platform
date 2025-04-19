using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Instructors.Commands.Models
{
    public class DeleteInstructorCommand : IRequest<Responses<string>>
    {
        public int Id { get; set; }
    }
}
