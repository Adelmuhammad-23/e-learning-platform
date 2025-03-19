using e_learning.Core.Bases;
using e_learning.Core.Features.Authentication.Queries.Models;
using e_learning.Data.Entities.Identity;
using e_learning.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponsesHandler,
        IRequestHandler<ConfirmResetPasswordQuery, Responses<string>>
    {

        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationServices _authenticationServices;

        #endregion

        #region
        public AuthenticationQueryHandler(UserManager<User> userManager,
                                            IAuthenticationServices authenticationServices)
        {
            _userManager = userManager;
            _authenticationServices = authenticationServices;
        }
        #endregion

        #region



        public async Task<Responses<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var resetPassword = await _authenticationServices.ConfirmResetPasswordAsync(request.Email, request.Code);
            switch (resetPassword)
            {
                case ("User is not found "):
                    return NotFound<string>("User  Is Not Found]");
                case ("Invalid Code"):
                    return BadRequest<string>("Invalid Code");
                case ("Success"):
                    return Success("Success");
                default:
                    return BadRequest<string>();

            }
            throw new NotImplementedException();
        }
        #endregion
    }
}
