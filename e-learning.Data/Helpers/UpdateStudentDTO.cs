using Microsoft.AspNetCore.Http;

namespace e_learning.Data.Helpers
{
    public class UpdateStudentDTO
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string Email { get; set; }
    }
}
