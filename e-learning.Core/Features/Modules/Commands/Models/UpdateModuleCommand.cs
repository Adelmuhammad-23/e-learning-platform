using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Models
{
    public class UpdateModuleCommand : IRequest<Responses<string>>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
    }
}
