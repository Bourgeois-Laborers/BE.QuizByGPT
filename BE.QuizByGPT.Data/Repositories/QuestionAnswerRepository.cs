using System;
using System.Threading.Tasks;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL.Repositories
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        private readonly ApplicationContext _context;

        public QuestionAnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<QuestionAnswerModel> CreateAsync(QuestionAnswerModel questionAnswer)
        {
            var createdQuestionAnswer = await _context.QuestionAnswer.AddAsync(questionAnswer);
            await _context.SaveChangesAsync();
            return createdQuestionAnswer.Entity;
        }

        public async Task<QuestionAnswerModel?> GetAsync(Guid id)
        {
            return await _context.QuestionAnswer.AsNoTracking().FirstOrDefaultAsync(model => model!.Id == id);
        }

        public async Task<QuestionAnswerModel> UpdateAsync(QuestionAnswerModel questionAnswer)
        {
            _context.QuestionAnswer.Update(questionAnswer);
            await _context.SaveChangesAsync();
            return questionAnswer;
        }

        public async Task DeleteAsync(Guid id)
        {
            var questionAnswer = await GetAsync(id);
            _context.QuestionAnswer.Remove(questionAnswer);
            await _context.SaveChangesAsync();
        }
    }
}
