using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class QuizSessionRepository : IQuizSessionRepository
    {
        private readonly ApplicationContext _context;

        public QuizSessionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<QuizSessionModel> CreateAsync(QuizSessionModel quizSession)
        {
            var createdQuizSession = await _context.QuizSession.AddAsync(quizSession);
            await _context.SaveChangesAsync();
            return createdQuizSession.Entity;
        }

        public async Task<QuizSessionModel?> GetAsync(Guid id)
        {
            return await _context.QuizSession
                .FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuizSessionModel?> GetExtendedAsync(Guid id)
        {
            return await _context.QuizSession
                .Include(model => model.Quiz)
                .Include(model => model.UserSessionQuizSession)
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuizSessionModel> UpdateAsync(QuizSessionModel quizSession)
        {
            _context.QuizSession.Update(quizSession);
            await _context.SaveChangesAsync();
            return quizSession;
        }

        public async Task DeleteAsync(Guid id)
        {
            var quizSession = await GetAsync(id);
            _context.QuizSession.Remove(quizSession);
            await _context.SaveChangesAsync();
        }
    }
}
