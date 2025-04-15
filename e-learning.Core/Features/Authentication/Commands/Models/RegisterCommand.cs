using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class RegisterCommand : IRequest<Responses<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
