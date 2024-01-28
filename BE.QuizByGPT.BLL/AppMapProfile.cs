using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
