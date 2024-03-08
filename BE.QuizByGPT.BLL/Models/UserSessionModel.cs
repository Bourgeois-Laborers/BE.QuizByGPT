namespace BE.QuizByGPT.BLL.Models
{
    public class UserSessionModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<QuizSessionModel>? QuizSessions { get; set; } = new ();
        public List<UserSessionQuizSessionModel>? UserSessionQuizSession { get; set; } = new ();
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    }

    public class UserSessionModelGetDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
