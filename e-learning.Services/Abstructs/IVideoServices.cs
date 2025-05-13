using e_learning.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{
    public interface IVideoServices
    {
        Task MarkVideoWatchedAsync(int studentId, int videoId);
        Task<bool> isWatched(int studentId, int videoId);

        public Task<string> AddVideoAsync(Video lesson, IFormFile videoFile);
    }
}
