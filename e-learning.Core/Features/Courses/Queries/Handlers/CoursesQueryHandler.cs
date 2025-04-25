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
        IRequestHandler<GetAllCoursesByCategoryIdQuery, Responses<List<AllCoursesByCategoryIdResponse>>>,
        IRequestHandler<GetTopPricedCoursesQuery, Responses<List<GetTopPricedCoursesResponse>>>,
        IRequestHandler<GetCourseById, Responses<GetCourseResponse>>,
        IRequestHandler<GetCourseByInstructorId, Responses<GetCourseResponse[]>>
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
            if (courses == null)
                return NotFound<List<AllCoursesResponse>>("Not found courses");


            var coursesMapping = _mapper.Map<List<AllCoursesResponse>>(courses);

            var result = Success(coursesMapping);
            result.Data = coursesMapping;
            result.Meta = new { TotalCourseCount = courses.Count };
            return result;

        }

        public async Task<Responses<List<AllCoursesByCategoryIdResponse>>> Handle(GetAllCoursesByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetCoursesByCategoryIdAsync(request.Id);
            if (courses == null)
                return NotFound<List<AllCoursesByCategoryIdResponse>>($"Not found courses this categoryId : {request.Id}");

            var coursesMapping = _mapper.Map<List<AllCoursesByCategoryIdResponse>>(courses);

            var result = Success(coursesMapping);
            result.Data = coursesMapping;
            result.Meta = new { TotalCourseCount = courses.Count };
            return result;
        }

        public async Task<Responses<List<GetTopPricedCoursesResponse>>> Handle(GetTopPricedCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetTopPricedCourses();
            if (courses == null)
                return NotFound<List<GetTopPricedCoursesResponse>>("Not found courses");


            var TopPricedCoursesMapping = _mapper.Map<List<GetTopPricedCoursesResponse>>(courses);


            var result = Success(TopPricedCoursesMapping);

            result.Data = TopPricedCoursesMapping;
            return result;
        }

        public async Task<Responses<GetCourseResponse>> Handle(GetCourseById request, CancellationToken cancellationToken)
        {
            var course = await _courseServices.GetCourseByIdAsync(request.Id);
            if (course == null)
                return NotFound<GetCourseResponse>($"No courses found with this ID:{request.Id}");
            var courseMapping = _mapper.Map<GetCourseResponse>(course);
            return Success(courseMapping);
        }

        public async Task<Responses<GetCourseResponse[]>> Handle(GetCourseByInstructorId request, CancellationToken cancellationToken)
        {
            var course = await _courseServices.GetCourseByInstructorIdAsync(request.Id);
            if (course == null)
                return NotFound<GetCourseResponse[]>($"No courses found with Instructor Id:{request.Id}");
            var courseMapping = _mapper.Map<GetCourseResponse[]>(course);
            return Success(courseMapping);
        }
        #endregion







    }
}
