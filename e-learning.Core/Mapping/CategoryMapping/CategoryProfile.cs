using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Core.Mapping.CategoryMapping
{
    public partial class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            GetAllCategoryMapping();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
