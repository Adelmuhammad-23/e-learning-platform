using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Models;
using e_learning.Core.Features.Courses.Queries.Responses;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Courses.Queries.Handlers
{
    public class CoursesQueryHandler : ResponsesHandler,
        IRequestHandler<GetAllCoursesQuery, Responses<List<AllCoursesResponse>>>,
        IRequestHandler<GetAllCoursesByCategoryIdQuery, Responses<List<AllCoursesByCategoryIdResponse>>>
    {
        #region Fields
        private readonly ICourseServices _courseServices;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CoursesQueryHandler(ICourseServices courseServices, IMapper mapper)
        {
            _courseServices = courseServices;
            _mapper = mapper;
        }
        #endregion

        #region Functions
        public async Task<Responses<List<AllCoursesResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetAllCoursesAsync();

            var coursesMapping = _mapper.Map<List<AllCoursesResponse>>(courses);

            var result = Success(coursesMapping);
            result.Data = coursesMapping;
            result.Meta = new { TotalCourseCount = courses.Count };
            return result;

        }

        public async Task<Responses<List<AllCoursesByCategoryIdResponse>>> Handle(GetAllCoursesByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetCoursesByCategoryIdAsync(request.Id);

            var coursesMapping = _mapper.Map<List<AllCoursesByCategoryIdResponse>>(courses);

            var result = Success(coursesMapping);
            result.Data = coursesMapping;
            result.Meta = new { TotalCourseCount = courses.Count };
            return result;
        }
        #endregion







    }
}
