using e_learning.Data.Entities.Identity;

namespace e_learning.Data.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public string? Position { get; set; }
        public List<string>? Certificates { get; set; }

        public bool isApproved { get; set; } = false;



        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public List<Course> Courses { get; set; } = new();
    }

}
