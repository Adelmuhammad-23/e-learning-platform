using e_learning.Core.Features.Authentication.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Commands.Validatiors
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {

        #region Constructors
        public SendResetPasswordValidator()
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

        }

        #endregion
    }
}
