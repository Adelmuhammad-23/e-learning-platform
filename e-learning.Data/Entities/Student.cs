namespace e_learning.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }

        // Navigation Properties
        public List<Reviews> Reviews { get; set; } = new();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }

}
