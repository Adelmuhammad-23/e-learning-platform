using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Modules.Queries.Models
{
    public class GetByCourseIdQuery : IRequest<Responses<List<GetByCourseIdResponse>>>
    {
        public GetByCourseIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
