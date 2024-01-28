using System;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces
{
    public interface IUserAnswerRepository
    {
        Task<UserAnswerModel> CreateAsync(UserAnswerModel userAnswer);
        Task<UserAnswerModel?> GetAsync(Guid id);
        Task<UserAnswerModel> UpdateAsync(UserAnswerModel userAnswer);
        Task DeleteAsync(Guid id);
    }
}
