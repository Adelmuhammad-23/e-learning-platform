using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Queries.Models;
using e_learning.Core.Features.Modules.Queries.Responses;
using e_learning.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace e_learning.Core.Features.Modules.Queries.Handlers
{
    public class ModuleCommandHandler : ResponsesHandler,
        IRequestHandler<GetByCourseIdQuery, Responses<List<GetByCourseIdResponse>>>
    {
        private readonly IModuleService _moduleService;
        private readonly ICourseServices _courseServices;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;
        public ModuleCommandHandler(IHttpContextAccessor httpContextAccessor, IModuleService moduleService, IMapper mapper, IEnrollmentService enrollmentService, ICourseServices courseServices)
        {
            _moduleService = moduleService;
            _mapper = mapper;
            _courseServices = courseServices;
            _enrollmentService = enrollmentService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Responses<List<GetByCourseIdResponse>>> Handle(GetByCourseIdQuery request, CancellationToken cancellationToken)
        {

            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            var studentId = int.Parse(userIdStr);
            var module = await _moduleService.GetByCourseIdAsync(request.Id);
            if (module == null)
                return NotFound<List<GetByCourseIdResponse>>("No found modules in this course");
            var modulesMapping = _mapper.Map<List<GetByCourseIdResponse>>(module);
            foreach (var courses in modulesMapping)
            {
                var checkStudentCourseEnrolled = await _enrollmentService.isEnrollment(studentId, courses.CourseId);
                if (!checkStudentCourseEnrolled)
                {
                    foreach (var video in courses.Videos)
                        video.Url = "You are not registered in this course.";
                }
            }
            var result = Success(modulesMapping);
            return result;
        }
    }
}
