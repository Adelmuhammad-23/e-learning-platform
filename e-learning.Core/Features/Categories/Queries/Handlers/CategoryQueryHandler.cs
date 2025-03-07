using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Categories.Queries.Models;
using e_learning.Core.Features.Categories.Queries.Responses;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Categories.Queries.Handlers
{
    public class CategoryQueryHandler : ResponsesHandler,
        IRequestHandler<GetAllCategoryQuery, Responses<List<GetAllCategoryResponse>>>
    {
        #region Fields
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CategoryQueryHandler(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }


        #endregion

        #region Functions
        public async Task<Responses<List<GetAllCategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryServices.GetCategoryList();
            if (categories == null)
                return NotFound<List<GetAllCategoryResponse>>("Not found categories");

            var categoriesMapping = _mapper.Map<List<GetAllCategoryResponse>>(categories);
            var result = Success(categoriesMapping);
            result.Data = categoriesMapping;
            return result;
        }

        #endregion
    }
}
