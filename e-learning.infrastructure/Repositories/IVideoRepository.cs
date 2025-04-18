using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IVideoRepository
    {
        public Task<string> Addvideo(Video video);
        public Task<Video> GetVideoByIdAsync(int id);
        Task DeleteVideoAsync(Video video);

    }
}
