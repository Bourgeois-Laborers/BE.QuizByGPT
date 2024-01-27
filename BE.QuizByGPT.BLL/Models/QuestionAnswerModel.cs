using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.QuizByGPT.BLL.Models
{
    public class QuestionAnswerModel
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
