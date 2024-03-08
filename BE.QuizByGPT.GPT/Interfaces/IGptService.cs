using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.GPT.Interfaces
{
    public interface IGptService
    {
        public Task<QuestionModel> GetNextQuestionAsync(Guid quizSessionId, int answersCount = 4, int correctAnswersCount = 1);
    }
}
