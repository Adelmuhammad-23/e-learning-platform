using e_learning.Core.Bases;
using e_learning.Data.Helpers;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Models
{
    public class AddVideoToModuleCommand : CreateVideoDto, IRequest<Responses<string>>
    {

    }
}
