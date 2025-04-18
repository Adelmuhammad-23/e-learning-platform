﻿using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Courses.Commands.Handlers
{
    public class CourseCommandHandler : ResponsesHandler,
        IRequestHandler<AddCourseCommand, Responses<string>>
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
            if (courseResult != null)
                return Success("Add Course is successfully");
            return BadRequest<string>("Failed to add course");
        }
        #endregion
    }
}
