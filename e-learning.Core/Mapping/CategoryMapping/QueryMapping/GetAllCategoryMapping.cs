using e_learning.Core.Features.Categories.Queries.Responses;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.CategoryMapping
{
    public partial class CategoryProfile
    {
        public void GetAllCategoryMapping()
        {
            CreateMap<Category, GetAllCategoryResponse>()
                .ForMember(dest => dest.CategoryId, src => src.MapFrom(i => i.Id))
                .ForMember(dest => dest.CategoryName, src => src.MapFrom(n => n.Name));
        }
    }
}
