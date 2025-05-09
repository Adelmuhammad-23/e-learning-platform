﻿using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;

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


        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return await _context.videos.FindAsync(id);
        }

        public async Task DeleteVideoAsync(Video video)
        {
            _context.videos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }
}
