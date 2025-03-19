using e_learning.Core.Bases;
using MediatR;

namespace e_learning.Core.Features.Authentication.Queries.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Responses<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
