using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Responses;
using MediatR;
namespace e_learning.Core.Features.Courses.Queries.Models
{
    public class GetAllCoursesQuery : IRequest<Responses<List<AllCoursesResponse>>>
    {

    }
}
