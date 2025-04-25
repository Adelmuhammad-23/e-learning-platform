using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Courses.Queries.Models
{
    public class GetCourseByInstructorId : IRequest<Responses<GetCourseResponse[]>>
    {
        public GetCourseByInstructorId(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}