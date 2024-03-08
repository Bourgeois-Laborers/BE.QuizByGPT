using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationContext _context;

        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<QuestionModel> CreateAsync(QuestionModel question)
        {
            var createdQuestion = await _context.Question.AddAsync(question);
            await _context.SaveChangesAsync();
            return createdQuestion.Entity;
        }

        public async Task<QuestionModel?> GetAsync(Guid id)
        {
            return await _context.Question.AsNoTracking().FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuestionModel> UpdateAsync(QuestionModel question)
        {
            _context.Question.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await GetAsync(id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
