using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Responses<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
