﻿using e_learning.Data.Entities.Identity;

namespace e_learning.Data.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public List<Course> Courses { get; set; } = new();
    }

}
