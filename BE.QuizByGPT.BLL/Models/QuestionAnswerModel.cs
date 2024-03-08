namespace BE.QuizByGPT.BLL.Models
{
    public class QuestionAnswerModel
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class QuestionAnswerModelGpt
    {
        public required string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
