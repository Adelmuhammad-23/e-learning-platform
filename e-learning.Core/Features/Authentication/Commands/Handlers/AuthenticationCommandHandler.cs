using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Authentication.Commands.Models;
using e_learning.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponsesHandler,
        IRequestHandler<RegisterCommand, Responses<string>>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion
        #region Handel Functions
        public async Task<Responses<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var checkUserName = await _userManager.FindByNameAsync(request.UserName);
            if (checkUserName is not null)
                return BadRequest<string>("User Name is already exist!");
            var checkUserEmail = await _userManager.FindByEmailAsync(request.Email);
            if (checkUserEmail is not null)
                return BadRequest<string>("Email is already exist!");

            var registerUser = await _userManager.CreateAsync(user, request.Password);

            if (!registerUser.Succeeded)
                return BadRequest<string>(registerUser.Errors.FirstOrDefault().Description);

            return Success("Register is successfully");

        }

        #endregion
    }
}
