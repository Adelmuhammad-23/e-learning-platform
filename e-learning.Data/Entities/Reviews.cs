namespace e_learning.Data.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
