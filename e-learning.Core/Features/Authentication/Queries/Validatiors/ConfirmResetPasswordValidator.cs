using e_learning.Core.Features.Authentication.Queries.Models;
using FluentValidation;

namespace e_learning.Core.Features.Authentication.Queries.Validatiors
{
    public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Constructors
        public ConfirmResetPasswordValidator()
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
            RuleFor(x => x.Code)
             .NotEmpty()
                .NotNull();
        }

        #endregion
    }
}
