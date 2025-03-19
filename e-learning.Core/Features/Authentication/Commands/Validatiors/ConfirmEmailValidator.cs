using e_learning.Core.Features.Authentication.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Commands.Validatiors
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
    {
        #region Constructors
        public ConfirmEmailValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Code)
                 .NotEmpty()
                .NotNull();
        }
        #endregion
    }
}
