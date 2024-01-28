using BE.QuizByGPT.BLL;
using BE.QuizByGPT.DAL;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.DAL.Repositories;
using BE.QuizByGPT.Interfaces;
using BE.QuizByGPT.Middlewares;
using BE.QuizByGPT.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        string.Format(builder.Configuration["ConnectionStrings:DefaultConnection"], Environment.GetEnvironmentVariable("DBUser"), Environment.GetEnvironmentVariable("DBPassword"))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddProfile(typeof(AppMapProfile)));

builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
builder.Services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();

builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizSessionService, QuizSessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<HeaderRequestIdMiddleware>();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<HeaderValidationMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.Migrate();
}

app.Run();
