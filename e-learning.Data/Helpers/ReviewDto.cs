namespace e_learning.Data.Helpers
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string StudentName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
