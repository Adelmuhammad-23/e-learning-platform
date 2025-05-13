using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace e_learning.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly Cloudinary _cloudinary;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ModuleService(
            IHttpContextAccessor httpContextAccessor,
        IVideoRepository videoRepository,
            IModuleRepository moduleRepository,
            IOptions<CloudinarySettings> config)
        {
            _videoRepository = videoRepository;
            _moduleRepository = moduleRepository;
            _httpContextAccessor = httpContextAccessor;

            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
            _cloudinary.Api.Timeout = 10 * 60 * 60 * 1000;
            _cloudinary.Api.Client.Timeout = TimeSpan.FromMinutes(10);
        }

        public async Task<string> AddModuleAsync(Module module)
        {
            await _moduleRepository.AddAsync(module);
            return "Success";
        }

        public async Task<string> AddVideoToModuleAsync(CreateVideoDto dto)
        {
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(dto.VideoFile.FileName, dto.VideoFile.OpenReadStream()),
                PublicId = $"modules/{Guid.NewGuid()}"

            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != HttpStatusCode.OK)
                return "Video upload failed";

            var durationInSeconds = uploadResult.Duration as double? ?? 0.0;


            var video = new Data.Entities.Video
            {
                Title = dto.Title,
                Url = uploadResult.SecureUrl.ToString(),
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                ModuleId = dto.ModuleId,
                PublicId = uploadResult.PublicId

            };

            await _videoRepository.Addvideo(video);

            return "Video uploaded and saved to module successfully";
        }
        public async Task<string> DeleteVideoFromModuleAsync(int videoId)
        {
            var video = await _videoRepository.GetVideoByIdAsync(videoId);
            if (video == null)
                return "Video not found";

            var deletionParams = new DeletionParams(video.PublicId)
            {
                ResourceType = ResourceType.Video
            };
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Result != "ok")
                return "Failed to delete from Cloudinary";

            await _videoRepository.DeleteVideoAsync(video);

            return "Video deleted successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var isExist = await _moduleRepository.ExistsAsync(id);
            if (!isExist)
                return "NotFound";

            await _moduleRepository.DeleteAsync(id);
            return "Deleted";
        }

        public async Task<string> UpdateAsync(Module module)
        {
            var isExist = await _moduleRepository.ExistsAsync(module.Id);
            if (!isExist)
                return "NotFound";
            await _moduleRepository.UpdateAsync(module);
            return "Updated";
        }

        public async Task<Module> GetModuleByIdAsync(int id)
        {


            var Module = await _moduleRepository.GetByIdAsync(id);
            if (Module == null)
                return null;
            return Module;
        }

        public async Task<List<Module>> GetByCourseIdAsync(int courseId)
        {
            var moduls = await _moduleRepository.GetByCourseIdAsync(courseId);
            return moduls;
        }
    }

}

