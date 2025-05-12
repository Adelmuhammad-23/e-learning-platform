namespace e_learning.Data.Entities
{
    public class StudentVideo
    {
        public int StudentId { get; set; }
        public int VideoId { get; set; }
        public bool IsWatched { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Video Video { get; set; }
    }

}
