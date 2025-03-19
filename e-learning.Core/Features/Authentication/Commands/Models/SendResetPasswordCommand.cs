using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Responses<string>>
    {
        public string Email { get; set; }
    }
}
