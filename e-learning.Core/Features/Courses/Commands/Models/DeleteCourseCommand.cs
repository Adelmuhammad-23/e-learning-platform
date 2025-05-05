using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Courses.Commands.Models
{
    public class DeleteCourseCommand : IRequest<Responses<string>>
    {
        public DeleteCourseCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
