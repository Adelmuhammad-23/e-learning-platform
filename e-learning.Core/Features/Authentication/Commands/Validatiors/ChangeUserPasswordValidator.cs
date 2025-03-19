using e_learning.Core.Features.Authentication.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Commands.Validatiors
{
    internal class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {

        #region Constructors
        public ChangeUserPasswordValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions

        public void ApplyValidationsRules()
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.CurrentPassword)
            .NotEmpty()
                .NotNull();
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .NotNull();

        }
        #endregion
    }
}
