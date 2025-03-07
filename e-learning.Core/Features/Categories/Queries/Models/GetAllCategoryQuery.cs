using e_learning.Core.Bases;
using e_learning.Core.Features.Categories.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Categories.Queries.Models
{
    public class GetAllCategoryQuery : IRequest<Responses<List<GetAllCategoryResponse>>>
    {
    }
}
