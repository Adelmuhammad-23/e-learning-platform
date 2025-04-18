using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.Extensions.Options;
using System.Net;

namespace e_learning.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly Cloudinary _cloudinary;

        public ModuleService(
            IVideoRepository videoRepository,
            IModuleRepository moduleRepository,
            IOptions<CloudinarySettings> config)
        {
            _videoRepository = videoRepository;
            _moduleRepository = moduleRepository;

            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
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
                // مفيش داعي تكتب ResourceType هنا لأنه بيتظبط تلقائي كـ "video"
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
                ModuleId = dto.ModuleId
            };

            await _videoRepository.Addvideo(video);

            return "Video uploaded and saved to module successfully";
        }
    }

}

