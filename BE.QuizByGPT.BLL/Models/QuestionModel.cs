namespace BE.QuizByGPT.BLL.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required List<QuestionAnswerModel> Answers { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class QuestionModelGpt
    {
        public string? Question { get; set; }
        public List<QuestionAnswerModelGpt>? Answers { get; set; }
        public string? Error { get; set; }
    }
}
