using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Models
{
    public class DeleteVideoFromModuleCommand : IRequest<Responses<string>>
    {
        public DeleteVideoFromModuleCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
