using e_learning.Core.Features.Courses.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Courses.Commands.Validatiors
{
    public class AddCourseValidator : AbstractValidator<AddCourseCommand>
    {
        #region Constructors

        public AddCourseValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.UpdatedAt)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.InstructorId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.InstructorId)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.CategoryId)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.Hours)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
        }
        #endregion
    }
}
