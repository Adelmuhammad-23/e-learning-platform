namespace e_learning.Core.Features.Courses.Queries.Responses
{
    public class GetCourseResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; } = 0;
        public DateTime UpdatedAt { get; set; }
        public List<ListOfModules> Modules { get; set; }
    }
    public class ListOfModules
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
