using e_learning.Data.Entities.Identity;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace e_learning.Services.Implementations
{
    public class AuthenticationServices : IAuthenticationServices
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        private readonly UserManager<User> _userManager;
        private readonly IUserRefreshTokenRepository _refreshTokenRepository;
        private readonly IEmailServices _emailServices;
        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constructors
        public AuthenticationServices(JwtSettings jwtSettings,
                                     IUserRefreshTokenRepository userRefreshTokenRepository,
                                     UserManager<User> userManager,
                                     IUserRefreshTokenRepository refreshTokenRepository,
                                     IEmailServices emailServices,
                                     ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _emailServices = emailServices;
            _dbContext = dbContext;
        }

        #endregion

        #region Handle Functions


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<JwtAuthResult> GetJwtToken(User user)
        {
            var (jwtToken, accessToken) = await GetJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);

            var refreshTokenResult = new UserRefreshToken
            {
                Token = accessToken,
                RefreshToken = refreshToken.TokenString,
                IsRevoked = false,
                IsUsed = true,
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserId = user.Id,
                JwtId = jwtToken.Id,
            };

            //add this data in UserRefreshTokenTable in database
            await _userRefreshTokenRepository.AddAsync(refreshTokenResult);

            var response = new JwtAuthResult();
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            return response;

        }
        #endregion

        #region Claims Functions
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.UserName)


            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        #endregion

        #region JWT Token Functions  For Help
        // using table to return more than one of types like string and JwtSecurityToken
        private async Task<(JwtSecurityToken, string)> GetJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
              _jwtSettings.Issuer,
              _jwtSettings.Audience,
                claims,
              expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            //token
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }


        #endregion

        #region Refresh Token Functions for Help
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                ExpierAt = DateTime.Now.AddMonths(_jwtSettings.RefreshTokenExpireDate),
                TokenString = GeneratRefreshToken(),
                UserName = userName
            };
            //if refreshtoken is exist => update if not Add
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, r) => refreshToken);
            return refreshToken;
        }
        private string GeneratRefreshToken()
        {
            var rondamNumber = new byte[32];
            var rondamNumberGenerated = RandomNumberGenerator.Create();
            rondamNumberGenerated.GetBytes(rondamNumber);
            return Convert.ToBase64String(rondamNumber);
        }


        public async Task<string> ConfirmEmailAsync(int userId, string code)
        {
            if (userId == null || code == null)
                return "Invalid UserId Or Code";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (!confirmEmail.Succeeded)
                return "Error When Confirm Email";
            return "Success";
        }

        public async Task<string> SendResetPasswordCodeAsync(string email)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return "User Not Found";
                //Generate random number to send in email
                Random generator = new Random();
                string randomNumber = generator.Next(0, 10000).ToString("D6");
                //update user in database
                user.Code = randomNumber;
                var updateUser = await _userManager.UpdateAsync(user);
                if (!updateUser.Succeeded)
                    return "Error When send code to Email";

                #region Send code in Email HTML template
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
                            .code {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #007bff;
                                background-color: #f8f9fa;
                                padding: 10px 20px;
                                border-radius: 5px;
                                display: inline-block;
                                margin: 10px 0;
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
                                <h2>Password Reset Request</h2>
                                <p>We received a request to reset your password. Use the following code to proceed:</p>
                                <span class='code'>{randomNumber}</span>
                                <p>If you did not request a password reset, you can ignore this email.</p>
                                <p class='footer'>This code will expire soon for security reasons.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
                #endregion

                await _emailServices.SendEmailAsync(user.Email, message, "Reset Your Password");

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPasswordAsync(string email, string code)
        {
            //user
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return "User is not found ";
            //code in database 
            //check code is equal or not
            if (user.Code != code)
                return "Invalid Code";
            return "Success";
        }

        public async Task<string> ResetPasswordAsync(string email, string Password)
        {
            var transact = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return "User is not found ";

                var removeOldPassword = await _userManager.RemovePasswordAsync(user);
                var updatePassword = await _userManager.AddPasswordAsync(user, Password);
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}