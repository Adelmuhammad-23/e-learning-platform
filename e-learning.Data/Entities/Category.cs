namespace e_learning.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
