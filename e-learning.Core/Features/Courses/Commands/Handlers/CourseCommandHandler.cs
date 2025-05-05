using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Courses.Commands.Handlers
{
    public class CourseCommandHandler : ResponsesHandler,
        IRequestHandler<AddCourseCommand, Responses<string>>,
        IRequestHandler<DeleteCourseCommand, Responses<string>>
    {
        #region Fields
        private readonly ICourseServices _courseServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public CourseCommandHandler(ICourseServices courseServices, IMapper mapper)
        {
            _courseServices = courseServices;
            _mapper = mapper;
        }
        #endregion

        #region Handel Functions
        public async Task<Responses<string>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var courseMapping = _mapper.Map<Course>(request);
            var courseResult = await _courseServices.AddCourse(courseMapping, request.Image);
            switch (courseResult)
            {
                case "Failed to get request context":
                    return BadRequest<string>("Failed to get request context");

                case "Not Authorized because Instructor Not Found":
                    return Unauthorized<string>("Not Authorized because Instructor Not Found");
                case "Success":
                    return Success("Success");
                default:
                    return BadRequest<string>("Failed to add course");
            }
        }

        public async Task<Responses<string>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseServices.GetCourseByIdAsync(request.Id);
            if (course == null)
                return NotFound<string>("Course not found");
            var courseDeleted = await _courseServices.DeleteCourseAsync(request.Id);
            if (courseDeleted == "Deleted")
                return Success("Deleted is successfully");
            return BadRequest<string>("Error when delete this course");
        }
        #endregion
    }
}
