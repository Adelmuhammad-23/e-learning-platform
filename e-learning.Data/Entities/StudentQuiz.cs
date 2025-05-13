namespace e_learning.Data.Entities
{
    public class StudentQuiz
    {
        public int StudentId { get; set; }
        public int QuizId { get; set; }
        public double Score { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Quiz Quiz { get; set; }
    }

}
