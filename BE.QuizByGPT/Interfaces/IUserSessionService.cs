using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.Interfaces;

public interface IUserSessionService
{
    Task<UserSessionModel> Get(Guid id);
    Task<UserSessionModel> Create(UserSessionModel userSessionGetDto);
}