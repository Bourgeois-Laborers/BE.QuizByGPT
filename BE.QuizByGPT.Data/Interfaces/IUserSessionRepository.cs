using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces;

public interface IUserSessionRepository
{
    Task<UserSessionModel> CreateAsync(UserSessionModel userSession);
    Task<UserSessionModel?> GetAsync(Guid id);
    Task<UserSessionModel> UpdateAsync(UserSessionModel userSession);
    Task DeleteAsync(Guid id);
}