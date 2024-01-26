using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Enums;

namespace BE.QuizByGPT.BLL.Models
{
    public class QuizModel
    {
        public Guid Id { get; set; }
        public required string Topic { get; set; }
        public QuizStatusEnum Status { get; set; }
        public int QuestionsCount { get; set; }
        public Guid CreatedBy { get; set; }
        public List<QuestionModel>? Questions { get; set; }
    }
}
