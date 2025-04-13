using e_learning.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{
    public interface IVideoServices
    {
        public Task<string> AddVideoAsync(Video lesson, IFormFile videoFile);
    }
}
