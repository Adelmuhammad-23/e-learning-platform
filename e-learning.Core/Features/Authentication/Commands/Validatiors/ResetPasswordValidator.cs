using e_learning.Core.Features.Authentication.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Commands.Validatiors
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {


        #region Constructors

        public ResetPasswordValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.confirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(x => x.Password).WithMessage("Password Not Equal Confirm Password");
        }
        #endregion
    }
}
