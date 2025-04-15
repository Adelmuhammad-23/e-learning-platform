using e_learning.Core.Features.Authentication.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Commands.Validatiors
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        #region Fields
        #endregion

        #region Constructors

        public RegisterValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.Email)
               .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.UserName)
              .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.RoleName)
              .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.Password)
              .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Password Not Equal Confirm Password]");

        }
        #endregion
    }
}