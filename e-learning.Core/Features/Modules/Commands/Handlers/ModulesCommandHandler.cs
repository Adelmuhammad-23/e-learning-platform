using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Handlers
{
    public class ModulesCommandHandler : ResponsesHandler,
        IRequestHandler<AddModuleCommand, Responses<string>>
    {
        private readonly IModuleService _moduleService;
        private readonly IMapper _mapper;
        public ModulesCommandHandler(IModuleService moduleService, IMapper mapper)
        {
            _moduleService = moduleService;
            _mapper = mapper;
        }
        public async Task<Responses<string>> Handle(AddModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleMapping = _mapper.Map<Module>(request);
            var result = await _moduleService.AddModuleAsync(moduleMapping);
            if (result == "Success")
                return Success("Add Module is Successfully");
            return BadRequest<string>("Failed to add Module");
        }
    }
}
