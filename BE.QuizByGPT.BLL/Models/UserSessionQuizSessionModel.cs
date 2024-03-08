namespace BE.QuizByGPT.BLL.Models
{
    public class UserSessionQuizSessionModel
    {
        public Guid UserSessionId { get; set; }
        public UserSessionModel UserSession { get; set; }
        public Guid QuizSessionId { get; set; }
        public QuizSessionModel QuizSession { get; set; }

        public bool IsUserReady { get; set; }
    }
}
