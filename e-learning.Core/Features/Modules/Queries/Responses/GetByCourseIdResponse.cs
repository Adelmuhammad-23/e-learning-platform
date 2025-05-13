namespace e_learning.Core.Features.Modules.Queries.Responses
{
    public class GetByCourseIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public List<ListOfVideos> Videos { get; set; }

        public List<ListOfQuizzesDto> Quizzes { get; set; }

    }
    public class ListOfVideos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ModuleId { get; set; }
        public bool isWatched { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class ListOfQuizzesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Score { get; set; }

        public int NumberOfQuestions { get; set; }
    }
}
