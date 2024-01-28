using System;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.Interfaces;

public interface IQuizService
{
    Task<QuizModel> Get(Guid id);
    Task<QuizModel> Create(Guid userSessionId, QuizModel quiz);
}
