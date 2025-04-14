using e_learning.Core.Features.Videos.Commands.Models;
using FluentValidation;

namespace e_learning.Core.Features.Videos.Commands.Validatiors
{
    public class AddVideoInCourseValidator : AbstractValidator<AddVideoInCourse>
    {
        #region Constructors

        public AddVideoInCourseValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handel Functions

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Duration)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.videoFile)
               .NotEmpty()
                .NotNull();
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull();
        }
        #endregion

    }
}
