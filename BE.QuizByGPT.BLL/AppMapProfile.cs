using AutoMapper;
using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.BLL
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<UserSessionModel, UserSessionModelGetDto>().ReverseMap();
            CreateMap<QuizModel, QuizModelGetDto>().ReverseMap();
            CreateMap<QuizModel, QuizModelPostDto>().ReverseMap();
            CreateMap<QuizSessionModel, QuizSessionModelGetDto>().ReverseMap();
            CreateMap<QuestionModel, QuestionModelGpt>()
                .ForMember("Question", opt => opt.MapFrom(gpt => gpt.Text))
                .ReverseMap()
                .ForMember("Text", opt => opt.MapFrom(gpt => gpt.Question));
            CreateMap<QuestionAnswerModel, QuestionAnswerModelGpt>().ReverseMap();
        }
    }
}
