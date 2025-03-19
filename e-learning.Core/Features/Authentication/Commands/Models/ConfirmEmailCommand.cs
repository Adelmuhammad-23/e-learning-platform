using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class ConfirmEmailCommand : IRequest<Responses<string>>
    {
        public int userId { get; set; }
        public string Code { get; set; }
    }
}
