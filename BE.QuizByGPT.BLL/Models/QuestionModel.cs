using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.QuizByGPT.BLL.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required List<QuestionAnswerModel> Answers { get; set; }
    }
}
