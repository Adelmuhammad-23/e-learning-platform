namespace e_learning.Data.Helpers
{
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public string? Position { get; set; }
        public List<string>? Certificates { get; set; }
        public bool? isApproved { get; set; }
    }
}
