using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Modules.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Modules.Commands.Handlers
{
    public class ModulesCommandHandler : ResponsesHandler,
        IRequestHandler<AddModuleCommand, Responses<string>>,
        IRequestHandler<AddVideoToModuleCommand, Responses<string>>,
        IRequestHandler<UpdateModuleCommand, Responses<string>>,
        IRequestHandler<DeleteVideoFromModuleCommand, Responses<string>>,
        IRequestHandler<DeleteModuleCommand, Responses<string>>
    {
        private readonly IModuleService _moduleService;
        private readonly IVideoServices _videoService;
        private readonly IMapper _mapper;
        public ModulesCommandHandler(IModuleService moduleService, IVideoServices videoService, IMapper mapper)
        {
            _moduleService = moduleService;
            _videoService = videoService;

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

        public async Task<Responses<string>> Handle(AddVideoToModuleCommand request, CancellationToken cancellationToken)
        {
            var result = await _moduleService.AddVideoToModuleAsync(request);
            switch (result)
            {
                case "Video upload failed":
                    return BadRequest<string>("Video upload failed");
                case "Video uploaded and saved to module successfully":
                    return Success("Video uploaded and saved to module successfully");
                default: return BadRequest<string>("Failed to add Module");
            }
        }

        public async Task<Responses<string>> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleMapping = _mapper.Map<Module>(request);
            var result = await _moduleService.UpdateAsync(moduleMapping);
            switch (result)
            {
                case "NotFound":
                    return NotFound<string>("NotFound");
                case "Updated":
                    return Success("Updated");
                default: return BadRequest<string>("Failed to update Module");
            }
        }

        public async Task<Responses<string>> Handle(DeleteVideoFromModuleCommand request, CancellationToken cancellationToken)
        {
            var videoExist = await _moduleService.DeleteVideoFromModuleAsync(request.Id);
            switch (videoExist)
            {
                case "Video not found":
                    return NotFound<string>("Video not found");
                case "Failed to delete from Cloudinary":
                    return BadRequest<string>("Failed to delete from Cloudinary");
                case "Video deleted successfully":
                    return Success("Video deleted successfully");

                default: return BadRequest<string>();
            }
        }

        public async Task<Responses<string>> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var course = await _moduleService.GetModuleByIdAsync(request.Id);
            if (course == null)
                return NotFound<string>("Course not found");
            var courseDeleted = await _moduleService.DeleteAsync(request.Id);
            if (courseDeleted == "Deleted")
                return Success("Deleted is successfully");
            return BadRequest<string>("Error when delete this course");
        }
    }
}
