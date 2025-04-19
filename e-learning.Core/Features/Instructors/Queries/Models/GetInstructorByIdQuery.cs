using e_learning.Core.Bases;
using e_learning.Core.Features.Instructors.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Instructors.Queries.Models
{
    public class GetInstructorByIdQuery : IRequest<Responses<InstructorQueryResponse>>
    {
        public int Id { get; set; }

        public GetInstructorByIdQuery(int id)
        {
            Id = id;
        }
    }
}