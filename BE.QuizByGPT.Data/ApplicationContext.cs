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
        public DbSet<UserSessionQuizSessionModel> UserSessionQuizSession { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserSessionModel>()
                .HasMany(c => c.QuizSessions)
                .WithMany(s => s.UserSessions)
                .UsingEntity<UserSessionQuizSessionModel>(
                    j => j
                        .HasOne(pt => pt.QuizSession)
                        .WithMany(t => t.UserSessionQuizSession)
                        .HasForeignKey(pt => pt.QuizSessionId),
                    j => j
                        .HasOne(pt => pt.UserSession)
                        .WithMany(p => p.UserSessionQuizSession)
                        .HasForeignKey(pt => pt.UserSessionId),
                    j =>
                    {
                        j.Property(pt => pt.IsUserReady).HasDefaultValue(false);
                        j.HasKey(t => new { t.QuizSessionId, t.UserSessionId });
                        j.ToTable("UserSessionQuizSession");
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
