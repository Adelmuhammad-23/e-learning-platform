using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Authentication.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Data.Entities.Identity;
using e_learning.Data.Helpers;
using e_learning.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponsesHandler,
        IRequestHandler<RegisterCommand, Responses<string>>,
        IRequestHandler<SignInCommand, Responses<JwtAuthResult>>,
         IRequestHandler<ConfirmEmailCommand, Responses<string>>,
        IRequestHandler<ResetPasswordCommand, Responses<string>>,
         IRequestHandler<ChangeUserPasswordCommand, Responses<string>>,
        IRequestHandler<SendResetPasswordCommand, Responses<string>>
    {
        #region Fields
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailServices _emailServices;
        private readonly IInstructorService _instructorService;
        private readonly IAuthenticationServices _authenticationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IHttpContextAccessor contextAccessor,
                                            IUrlHelper urlHelper,
                                            UserManager<User> userManager,
                                            RoleManager<Role> roleManager,
                                            IEmailServices emailServices,
                                            SignInManager<User> signInManager,
                                            IMapper mapper,
                                            IAuthenticationServices authenticationService,
                                            IInstructorService instructorService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _urlHelper = urlHelper;
            _emailServices = emailServices;
            _instructorService = instructorService;

        }
        #endregion
        #region Handel Functions
        public async Task<Responses<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _authenticationService.BeginTransactionAsync();
            try
            {
                var user = _mapper.Map<User>(request);

                var checkUserName = await _userManager.FindByNameAsync(request.UserName);
                if (checkUserName is not null)
                    return BadRequest<string>("User Name is already exist!");
                var checkUserEmail = await _userManager.FindByEmailAsync(request.Email);
                if (checkUserEmail is not null)
                    return BadRequest<string>("Email is already exist!");

                var registerUser = await _userManager.CreateAsync(user, request.Password);

                var role = await _roleManager.FindByNameAsync(request.RoleName.ToLowerInvariant());
                if (request.RoleName is null)
                    return Success("Role is not valid");

                await _userManager.AddToRoleAsync(user, request.RoleName);

                if (request.RoleName.Equals("Instructor", StringComparison.InvariantCultureIgnoreCase))
                {
                    var instructor = new Instructor
                    {
                        UserId = user.Id,
                        Name = request.UserName,
                        Email = request.Email,
                        Bio = "Please enter your Bio",
                        Image = null
                    };
                    await _instructorService.AddInstructorAsync(instructor);
                }
                else if (request.RoleName.Equals("Student", StringComparison.InvariantCultureIgnoreCase))
                {

                }
                await _authenticationService.SaveChangesAsync();
                await _authenticationService.CommitAsync();

                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}{_urlHelper.Action("ConfirmEmail", "Auth", new { userId = user.Id, code = code })}";

                #region Email Confirmation HTML template
                var message = $@"
                <html>
                <head>
                    <style>
                        .email-container {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            padding: 20px;
                            text-align: center;
                        }}
                        .email-box {{
                            background: white;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                            display: inline-block;
                        }}
                        .button {{
                            display: inline-block;
                            background-color: #007bff;
                            color: white;
                            padding: 10px 20px;
                            text-decoration: none;
                            border-radius: 5px;
                            font-size: 16px;
                            font-weight: bold;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 12px;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='email-box'>
                            <h2>Confirm Your Email</h2>
                            <p>Welcome to <strong>Xcelerate Platform</strong>! Please confirm your email address to get started.</p>
                            <a href='{returnUrl}' class='button'>Confirm Email</a>
                            <p class='footer'>If you didn’t request this, you can safely ignore this email.</p>
                        </div>
                    </div>
                </body>
                </html>";
                #endregion

                await _emailServices.SendEmailAsync(user.Email, message, "Confirm Your Email");

                if (!registerUser.Succeeded)
                    return BadRequest<string>(registerUser.Errors.FirstOrDefault().Description);
                return Success("Register is successfully");
            }
            catch (Exception ex)
            {
                await _authenticationService.RollbackAsync();
                return BadRequest<string>($"Registration failed: {ex.Message}");
            }

        }
        public async Task<Responses<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByEmailAsync(request.Email);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>("User Name Is Not Exist");
            //try To Sign in 
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if Failed Return Passord is wrong
            if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>("Password Is Not Correct");

            //email confirmed
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>("Email Is Not Confirmed");
            //Generate Token
            var result = await _authenticationService.GetJwtToken(user);
            //return Token 
            return Success(result);

        }

        public async Task<Responses<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //return NotFound
            if (user == null)
                return NotFound<string>();
            var newUser = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest<string>("Password Not Equal Confirm Password]");

            if (!newUser.Succeeded)
                return BadRequest<string>(newUser.Errors.FirstOrDefault().Description);

            return Success("");

        }

        public async Task<Responses<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var sendCode = await _authenticationService.SendResetPasswordCodeAsync(request.Email);
            switch (sendCode)
            {
                case ("User Not Found"):
                    return NotFound<string>("User Is Not Found");
                case ("Error When send code to Email"):
                    return BadRequest<string>("Error When send code to Email");
                case ("Success"):
                    return Success("Success");
                default:
                    return BadRequest<string>();
            }
        }
        public async Task<Responses<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmailAsync(request.userId, request.Code);
            switch (confirmEmail)
            {
                case "Invalid UserId Or Code":
                    return UnprocessableEntity<string>("Unprocessable Entity");
                case "Error When Confirm Email":
                    return BadRequest<string>("([SharedResourcesKeys.BadRequest]");
                default:
                    return Success("Success");
            }
        }

        public async Task<Responses<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetPassword = await _authenticationService.ResetPasswordAsync(request.Email, request.Password);
            switch (resetPassword)
            {
                case "User is not found ":
                    return NotFound<string>("UserIsNotFound]");
                case "Failed":
                    return BadRequest<string>("Bad Request]");
                default:
                    return Success("Success");
            }
        }
        #endregion
    }
}
