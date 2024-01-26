using BE.QuizByGPT.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.QuizByGPT.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserSessionModel> UserSession { get; set; }
        public DbSet<QuizModel> Quiz { get; set; }
        public DbSet<QuestionModel> Question { get; set; }
        public DbSet<QuestionAnswerModel> QuestionAnswer { get; set; }
        public DbSet<UserAnswerModel> UserAnswer { get; set; }
        public DbSet<QuizSessionModel> QuizSession { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
