namespace e_learning.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int CourseId { get; set; }
        public TimeSpan Duration { get; set; }

        // Navigation Property
        public Course Course { get; set; }
    }

}
