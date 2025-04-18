using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Models
{
    public class AddModuleCommand : IRequest<Responses<string>>
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
    }
}
