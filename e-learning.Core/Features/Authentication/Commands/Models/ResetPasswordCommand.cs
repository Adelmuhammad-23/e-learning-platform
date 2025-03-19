using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Responses<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string confirmPassword { get; set; }

    }
}
