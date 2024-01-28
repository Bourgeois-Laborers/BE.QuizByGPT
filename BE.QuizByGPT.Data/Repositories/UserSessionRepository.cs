using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ApplicationContext _context;

        public UserSessionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserSessionModel> CreateAsync(UserSessionModel userSession)
        {
            var createdUserSession = await _context.UserSession.AddAsync(userSession);
            await _context.SaveChangesAsync();
            return createdUserSession.Entity;
        }

        public async Task<UserSessionModel?> GetAsync(Guid id)
        {
            return await _context.UserSession
                .AsNoTracking()
                .Include(model => model.UserSessionQuizSession)
                .FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<UserSessionModel> UpdateAsync(UserSessionModel userSession)
        {
            _context.UserSession.Update(userSession);
            await _context.SaveChangesAsync();
            return userSession;
        }

        public async Task DeleteAsync(Guid id)
        {
            var userSession = await GetAsync(id);
            _context.UserSession.Remove(userSession);
            await _context.SaveChangesAsync();
        }
    }
}
