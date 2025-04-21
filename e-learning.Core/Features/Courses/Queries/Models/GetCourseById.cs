using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Courses.Queries.Models
{
    public class GetCourseById : IRequest<Responses<GetCourseResponse>>
    {
        public GetCourseById(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
