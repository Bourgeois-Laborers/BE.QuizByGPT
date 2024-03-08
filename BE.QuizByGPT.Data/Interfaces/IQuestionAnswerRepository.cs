using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces
{
    public interface IQuestionAnswerRepository
    {
        Task<QuestionAnswerModel> CreateAsync(QuestionAnswerModel questionAnswer);
        Task<QuestionAnswerModel?> GetAsync(Guid id);
        Task<QuestionAnswerModel> UpdateAsync(QuestionAnswerModel questionAnswer);
        Task DeleteAsync(Guid id);
    }
}
