﻿namespace e_learning.Data.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public List<Question> Questions { get; set; }
    }
}
