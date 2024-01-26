using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.QuizByGPT.BLL.Models
{
    public class UserSessionModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? QuizSessionId { get; set; }
        public QuizSessionModel? QuizSession { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
