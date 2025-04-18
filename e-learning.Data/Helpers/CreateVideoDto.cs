using Microsoft.AspNetCore.Http;

namespace e_learning.Data.Helpers
{
    public class CreateVideoDto
    {
        public string Title { get; set; }
        public int ModuleId { get; set; }
        public IFormFile VideoFile { get; set; }
    }

}
