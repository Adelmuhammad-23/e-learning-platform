using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IVideoRepository
    {

        Task<StudentVideo> GetStudentVideoAsync(int studentId, int videoId);
        Task<bool> isWatched(int studentId, int videoId);

        Task AddStudentVideoAsync(StudentVideo studentVideo);
        public Task<string> Addvideo(Video video);
        public Task<Video> GetVideoByIdAsync(int id);
        Task DeleteVideoAsync(Video video);
        public Task SaveChangesAsync();

    }
}
