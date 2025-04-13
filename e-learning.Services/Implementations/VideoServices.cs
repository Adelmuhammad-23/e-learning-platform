using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Implementations
{
    public class VideoServices : IVideoServices
    {
        #region 
        private readonly IVideoRepository _videoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;

        #endregion
        #region Vonstructor
        public VideoServices(IVideoRepository videoRepository,
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost)
        {
            _videoRepository = videoRepository;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
        }
        #endregion
        #region Handel Functions
        public async Task<string> AddVideoAsync(Video video, IFormFile videoFile)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return "Failed to get request context";

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "videos");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(videoFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/videos/{uniqueFileName}";

            video.Url = fileUrl;
            await _videoRepository.Addvideo(video);

            return "Success";
        }
        #endregion

    }
}

