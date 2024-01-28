using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Enums;

namespace BE.QuizByGPT.BLL.Models
{
    public class QuizSessionModel
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public QuizModel Quiz { get; set; }
        public List<UserSessionModel>? UserSessions { get; set; }
        public List<UserSessionQuizSessionModel>? UserSessionQuizSession { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public QuizSessionStatusEnum Status { get; set; }
        public Guid? CurrentQuestionId { get; set; }
        public QuestionModel? CurrentQuestion { get; set; }
    }

    public class QuizSessionModelGetDto
    {
        public Guid Id { get; set; }
        public QuizModelGetDto Quiz { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public QuizSessionStatusEnum Status { get; set; }
        public Guid? CurrentQuestionId { get; set; }
    }
}
