namespace e_learning.Data.Helpers
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public int StudentId { get; set; }
        public List<CartItemDto> Courses { get; set; } = new();
    }
    public class CartItemDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public decimal Price { get; set; }
    }

}
