namespace e_learning.Core.Features.Courses.Queries.Responses
{
    public class AllCoursesResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }

        public string Description { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }
    }
}
