using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Queries.Models;
using e_learning.Core.Features.Modules.Queries.Responses;
using e_learning.infrastructure.Repositories;
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
        private readonly IVideoServices _videoServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQuizRepository _quizRepository;

        private readonly IMapper _mapper;
        public ModuleCommandHandler(IQuizRepository quizRepository, IVideoServices videoServices, IHttpContextAccessor httpContextAccessor, IModuleService moduleService, IMapper mapper, IEnrollmentService enrollmentService, ICourseServices courseServices)
        {
            _videoServices = videoServices;
            _moduleService = moduleService;
            _mapper = mapper;
            _courseServices = courseServices;
            _enrollmentService = enrollmentService;
            _quizRepository = quizRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Responses<List<GetByCourseIdResponse>>> Handle(GetByCourseIdQuery request, CancellationToken cancellationToken)
        {


            var module = await _moduleService.GetByCourseIdAsync(request.Id);
            if (module == null)
                return NotFound<List<GetByCourseIdResponse>>("No found modules in this course");
            var modulesMapping = _mapper.Map<List<GetByCourseIdResponse>>(module);


            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                editVideos(false, modulesMapping, "not signed in");
                return Success(modulesMapping);
            }

            string role = _httpContextAccessor.HttpContext?.User?.FindFirst(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            if (role == "Instructor")
            {
                var insIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("instructorId")?.Value;
                int insId = int.Parse(insIdStr);
                var course = await _courseServices.GetCourseByIdAsync(request.Id);
                if (course is null) return NotFound<List<GetByCourseIdResponse>>("No found modules in this course");
                if (course.InstructorId == insId)
                {
                    return Success(modulesMapping);
                }
                else editVideos(false, modulesMapping, "you are not enrolled in this course");
            }

            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            var userId = int.Parse(userIdStr);
            foreach (var course in modulesMapping)
            {
                foreach (var video in course.Videos)
                {
                    var check = await _videoServices.isWatched(userId, video.Id);
                    if (check == true)
                        video.isWatched = true;
                    else
                        video.isWatched = false;
                }

            }
            foreach (var quizDto in modulesMapping)
            {
                foreach (var quizz in quizDto.Quizzes)
                {
                    var quize = await _quizRepository.GetByTitleAsync(quizDto.Title);
                    var score = await _quizRepository.GetScore(userId, quizz.Id);
                    quizz.Score = score;
                }

            }
            var checkStudentCourseEnrolled = await _enrollmentService.isEnrollment(userId, request.Id);
            editVideos(checkStudentCourseEnrolled, modulesMapping, "you are not enrolled in this course");
            var result = Success(modulesMapping);
            return result;
        }

        public void editVideos(bool checkStudentCourseEnrolled, List<GetByCourseIdResponse> modulesMapping, string message)
        {
            if (!checkStudentCourseEnrolled)
            {
                foreach (var modules in modulesMapping)
                {
                    foreach (var video in modules.Videos)
                    {
                        video.Url = message;
                    }
                }
            }
        }
    }
}
