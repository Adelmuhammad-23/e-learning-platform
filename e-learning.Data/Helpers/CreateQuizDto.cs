namespace e_learning.Data.Helpers
{
    public class CreateQuizDto
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int ModuleId { get; set; }
        public double Score { get; set; }

        public string? Message { get; set; }

        public List<CreateQuestionDto> Questions { get; set; }
    }

    public class CreateQuestionDto
    {
        public string Text { get; set; }
        public List<CreateChoiceDto> Choices { get; set; }
    }

    public class CreateChoiceDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class QuizWithQuestionsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<QuestionDto> Questions { get; set; } = new();

        public List<CreateChoiceDto> Choices { get; set; } = new();
    }
}
