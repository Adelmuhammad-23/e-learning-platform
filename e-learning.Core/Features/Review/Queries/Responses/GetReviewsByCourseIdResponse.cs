namespace e_learning.Core.Features.Review.Queries.Responses
{
    public class GetReviewsByCourseIdResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public string Image { get; set; }
        public int CourseId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
