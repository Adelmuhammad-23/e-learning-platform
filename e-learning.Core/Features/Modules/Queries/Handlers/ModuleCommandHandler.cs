using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Queries.Models;
using e_learning.Core.Features.Modules.Queries.Responses;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Modules.Queries.Handlers
{
    public class ModuleCommandHandler : ResponsesHandler,
        IRequestHandler<GetByCourseIdQuery, Responses<List<GetByCourseIdResponse>>>
    {
        private readonly IModuleService _moduleService;
        private readonly ICourseServices _courseServices;
        private readonly IMapper _mapper;
        public ModuleCommandHandler(IModuleService moduleService, IMapper mapper, ICourseServices courseServices)
        {
            _moduleService = moduleService;
            _mapper = mapper;
            _courseServices = courseServices;
        }
        public async Task<Responses<List<GetByCourseIdResponse>>> Handle(GetByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var module = await _moduleService.GetByCourseIdAsync(request.Id);
            if (module == null)
                return NotFound<List<GetByCourseIdResponse>>("No found modules in this course");
            var modulesMapping = _mapper.Map<List<GetByCourseIdResponse>>(module);
            var result = Success(modulesMapping);
            return result;
        }
    }
}
