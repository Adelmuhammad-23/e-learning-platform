using e_learning.Core.Features.Courses.Queries.Responses;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.CoursesMapping
{
    public partial class CoursesProfile
    {
        public void GetAllCoursesMapping()
        {
            CreateMap<Course, AllCoursesResponse>();
        }
    }
}
