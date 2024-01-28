using System;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ApplicationContext _context;

        public UserAnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserAnswerModel> CreateAsync(UserAnswerModel userAnswer)
        {
            var createdUserAnswer = await _context.UserAnswer.AddAsync(userAnswer);
            await _context.SaveChangesAsync();
            return createdUserAnswer.Entity;
        }

        public async Task<UserAnswerModel?> GetAsync(Guid id)
        {
            return await _context.UserAnswer.AsNoTracking().FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<UserAnswerModel> UpdateAsync(UserAnswerModel userAnswer)
        {
            _context.UserAnswer.Update(userAnswer);
            await _context.SaveChangesAsync();
            return userAnswer;
        }

        public async Task DeleteAsync(Guid id)
        {
            var userAnswer = await GetAsync(id);
            _context.UserAnswer.Remove(userAnswer);
            await _context.SaveChangesAsync();
        }
    }
}
