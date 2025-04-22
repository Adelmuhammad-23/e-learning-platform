namespace e_learning.Data.Helpers
{
    public class CreateReviewDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

}
