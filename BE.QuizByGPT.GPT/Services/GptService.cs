using BE.QuizByGPT.GPT.Interfaces;
using System.Text.Json;
using AutoMapper;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI_API;
using OpenAI_API.Chat;
using System.Text.Encodings.Web;
using BE.QuizByGPT.BLL.Enums;
using BE.QuizByGPT.GPT.Exceptions;

namespace BE.QuizByGPT.GPT.Services
{
    public class GptService : IGptService
    {
        private readonly ILogger<GptService> _logger;
        private readonly IConfiguration _configuration;
        private readonly OpenAIAPI _openAi;
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizSessionRepository _quizSessionRepository;

        public GptService(ILogger<GptService> logger, IConfiguration configuration, IMapper mapper, IQuizRepository quizRepository, IQuizSessionRepository quizSessionRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizSessionRepository = quizSessionRepository;
            _openAi = new OpenAIAPI();
        }

        public async Task<QuestionModel> GetNextQuestionAsync(Guid quizSessionId, int answersCount = 4, int correctAnswersCount = 1)
        {
            var quizSession = await _quizSessionRepository.GetExtendedAsync(quizSessionId);
            
            if (quizSession == null)
            {
                throw new Exception("QuizSession not found.");
            }

            var quiz = await _quizRepository.GetExtendedAsync(quizSession.QuizId);

            if (quiz == null)
            {
                throw new Exception("Quiz not found.");
            }

            if (quiz.Questions?.Count >= quiz.QuestionsCount)
            {
                throw new QuizQuestionCountLimitException("The quiz has already reached the maximum number of questions.");
            }

            quiz.Status = QuizStatusEnum.Generating;
            await _quizRepository.UpdateAsync(quiz);

            var messages = new List<ChatMessage>
            {
                new()
                {
                    Role = ChatMessageRole.System,
                    TextContent = string.Format(_configuration["GPTSettings:PromptForGenerate1Question"], answersCount, answersCount - correctAnswersCount, correctAnswersCount) + _configuration["GPTSettings:JsonFormat"]
                },
                new()
                {
                    Role = ChatMessageRole.User,
                    TextContent = quiz.Topic
                }
            };

            if (quiz.Questions?.Count > 0)
            {
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                messages.AddRange(
                    quiz.Questions.Select(question =>
                        JsonSerializer.Serialize(_mapper.Map<QuestionModelGpt>(question), jsonSerializerOptions)).Select(questionJson =>
                        new ChatMessage { Role = ChatMessageRole.Assistant, TextContent = questionJson }));
            }

            messages.Add(new ChatMessage
            {
                Role = ChatMessageRole.User,
                TextContent = "Generate the next question."
            });

            var request = new ChatRequest
            {
                Model = _configuration["GPTSettings:Model"],
                MaxTokens = 400,
                Temperature = 1,
                ResponseFormat = ChatRequest.ResponseFormats.JsonObject,
                Messages = messages
            };

            var response = await _openAi.Chat.CreateChatCompletionAsync(request);

            var textResponse = response.Choices.First().Message.TextContent;

            QuestionModel? questionModel;

            try
            {
                var questionModelGpt = JsonSerializer.Deserialize<QuestionModelGpt>(textResponse);
                if (!string.IsNullOrEmpty(questionModelGpt?.Error))
                {
                    throw new Exception($"The model returned an error in the generated question: \n {questionModelGpt.Error}");
                }

                questionModel = _mapper.Map<QuestionModel>(questionModelGpt);
            }
            catch (Exception e)
            {
                _logger.LogError("The model returned invalid JSON. " + e.Message);
                throw new Exception("The model returned invalid JSON. " + e.Message);
            }

            quiz.Questions ??= [];
            quiz.Questions.Add(questionModel);
            quiz.Status = QuizStatusEnum.Ready;

            quiz = await _quizRepository.UpdateAsync(quiz);

            return quiz.Questions!.OrderBy(model => model.CreatedAt).Last();
        }
    }
}
