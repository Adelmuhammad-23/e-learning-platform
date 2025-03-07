namespace e_learning.Data.Entities.Views
{
    public class TopPricedCourses
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }
    }
}
