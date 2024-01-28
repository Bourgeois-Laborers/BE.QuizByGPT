using AutoMapper;
using Azure;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.QuizByGPT.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionService(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public async Task<UserSessionModel> Get(Guid id)
        {
            var userSession = await _userSessionRepository.GetAsync(id);
            if (userSession == null)
            {
                throw new Exception("UserSessionId invalid.");
            }

            return userSession;
        }

        public async Task<UserSessionModel> Create(UserSessionModel userSession)
        {
            var createdUserSession = await _userSessionRepository.CreateAsync(userSession);

            return createdUserSession;
        }
    }
}
