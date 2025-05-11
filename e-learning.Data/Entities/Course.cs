namespace e_learning.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorEmail { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }

        // Navigation Properties
        public List<Module> Modules { get; set; } = new();

        public Category Category { get; set; }
        public List<Reviews> Reviews { get; set; } = new();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }

}
