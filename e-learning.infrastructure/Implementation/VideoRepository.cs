using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class VideoRepository : IVideoRepository
    {
        #region 
        private readonly ApplicationDbContext _context;
        #endregion
        #region Vonstructor
        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handel Functions
        public async Task<string> Addvideo(Video video)
        {
            await _context.videos.AddAsync(video);
            await _context.SaveChangesAsync();
            return "Success";
        }
        #endregion
        public async Task<StudentVideo> GetStudentVideoAsync(int studentId, int videoId)
     => await _context.StudentVideos.FindAsync(studentId, videoId);

        public async Task AddStudentVideoAsync(StudentVideo studentVideo)
            => await _context.StudentVideos.AddAsync(studentVideo);

        public async Task SaveChangesAsync()
       => await _context.SaveChangesAsync();
        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return await _context.videos.FindAsync(id);
        }

        public async Task DeleteVideoAsync(Video video)
        {
            _context.videos.Remove(video);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isWatched(int studentId, int videoId)
        {
            var check = await _context.StudentVideos.AsNoTracking().Where(si => si.StudentId.Equals(studentId) && si.VideoId.Equals(videoId)).FirstOrDefaultAsync();
            if (check != null)
            {
                if (check.IsWatched)
                    return true;
            }
            return false;
        }
    }
}
