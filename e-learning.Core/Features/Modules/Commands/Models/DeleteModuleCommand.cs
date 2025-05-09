using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Models
{
    public class DeleteModuleCommand : IRequest<Responses<string>>
    {
        public DeleteModuleCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
