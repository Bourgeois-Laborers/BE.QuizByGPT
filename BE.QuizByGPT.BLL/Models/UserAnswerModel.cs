using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.QuizByGPT.BLL.Models
{
    public class UserAnswerModel
    {
        public Guid Id { get; set; }
        public Guid AnswerId { get; set; }
        public required QuestionAnswerModel Answer { get; set; }
        public Guid UserSessionId { get; set; }
        public required UserSessionModel UserSession { get; set; }
    }
}
