using e_learning.Core.Features.Courses.Commands.Models;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.CoursesMapping
{
    public partial class CoursesProfile
    {
        public void AddCourseMapping()
        {
            CreateMap<AddCourseCommand, Course>();
        }

    }
}
