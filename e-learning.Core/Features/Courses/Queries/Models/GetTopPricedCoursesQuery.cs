using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Courses.Queries.Models
{
    public class GetTopPricedCoursesQuery : IRequest<Responses<List<GetTopPricedCoursesResponse>>>
    {
    }
}
