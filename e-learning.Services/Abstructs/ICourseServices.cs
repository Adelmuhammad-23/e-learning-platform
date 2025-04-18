﻿using e_learning.Data.Entities;
using e_learning.Data.Entities.Views;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{
    public interface ICourseServices
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<List<Course>> GetCoursesByCategoryIdAsync(int id);
        public Task<List<TopPricedCourses>> GetTopPricedCourses();
        public Task<string> AddCourse(Course course, IFormFile videoFile);
    }
}
