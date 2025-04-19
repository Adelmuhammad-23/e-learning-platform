using e_learning.Core.Bases;
using e_learning.Core.Features.Instructors.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Instructors.Queries.Models
{
    public class GetAllInstructorsQuery : IRequest<Responses<List<InstructorQueryResponse>>>
    {
    }
}
