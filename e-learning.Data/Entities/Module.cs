namespace e_learning.Data.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }

        // Navigation Properties
        public Course Course { get; set; }
        public List<Video> Videos { get; set; } = new();

        public List<Quiz> Quizzes { get; set; } = new();

    }
}
