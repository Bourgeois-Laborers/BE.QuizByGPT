using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces
{
    public interface IQuestionRepository
    {
        Task<QuestionModel> CreateAsync(QuestionModel question);
        Task<QuestionModel?> GetAsync(Guid id);
        Task<QuestionModel> UpdateAsync(QuestionModel question);
        Task DeleteAsync(Guid id);
    }
}
