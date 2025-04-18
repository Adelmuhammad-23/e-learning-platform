using AutoMapper;

namespace e_learning.Core.Mapping.CoursesMapping
{
    public partial class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            GetAllCoursesMapping();
            GetAllCoursesByCategoryIdMapping();
            GetTopPricedCoursesMapping();
            AddCourseMapping();
        }
    }
}
