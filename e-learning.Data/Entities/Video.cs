namespace e_learning.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool isWatched { get; set; }

        public int ModuleId { get; set; }
        public TimeSpan Duration { get; set; }
        public string PublicId { get; set; }


        // Navigation Property
        public Module Module { get; set; }
        public List<StudentVideo> StudentVideos { get; set; }

    }

}
