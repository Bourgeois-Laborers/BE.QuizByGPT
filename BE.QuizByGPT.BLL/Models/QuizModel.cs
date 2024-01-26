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
        public DateTime CreationDate { get; set; }
        public QuizStateEnum State { get; set; }
        public List<QuestionModel>? Questions { get; set; }
    }
}
