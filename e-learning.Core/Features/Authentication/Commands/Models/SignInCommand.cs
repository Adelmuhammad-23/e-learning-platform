using e_learning.Core.Bases;
using e_learning.Data.Helpers;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Responses<JwtAuthResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
