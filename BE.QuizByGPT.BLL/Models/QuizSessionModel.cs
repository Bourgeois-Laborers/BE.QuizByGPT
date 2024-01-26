﻿using System;
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
        public required QuizModel Quiz { get; set; }
        public List<UserSessionModel>? UserSessions { get; set; }
        public DateTime? StartDate { get; set; }
        public QuizSessionStateEnum State { get; set; }
        public Guid? CurrentQuestionId { get; set; }
        public QuestionModel? CurrentQuestion { get; set; }
    }
}
