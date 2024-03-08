using BE.QuizByGPT.BLL.Enums;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationContext _context;

        public QuizRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<QuizModel> CreateAsync(QuizModel quiz)
        {
            var createdQuiz = await _context.Quiz.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return createdQuiz.Entity;
        }

        public async Task<QuizModel?> GetAsync(Guid id)
        {
            return await _context.Quiz.AsNoTracking().FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuizModel?> GetExtendedAsync(Guid id)
        {
            return await _context.Quiz
                .Include(model => model.Questions)!
                .ThenInclude(model => model.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuizModel> UpdateAsync(QuizModel quiz)
        {
            _context.Quiz.Update(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task DeleteAsync(Guid id)
        {
            var quiz = await GetAsync(id);
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();
        }
        
        public async Task<QuizModel> UpdateQuizStatusAsync(Guid id, QuizStatusEnum status)
        {
            var quiz = await GetAsync(id);
            if (quiz == null)
            {
                throw new Exception("Quiz not found.");
            }
            
            quiz.Status = status;
            return await UpdateAsync(quiz);
        }
    }
}
