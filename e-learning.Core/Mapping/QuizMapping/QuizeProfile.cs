using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Core.Mapping.QuizMapping
{
    internal class QuizeProfileProfile : Profile
    {
        public QuizeProfileProfile()
        {
            CreateMap<Quiz, CreateQuizDto>().ForMember((dist)=>dist.Questions,(src)=>src.MapFrom((c)=>c.Questions));
            CreateMap<Question, QuestionDto>();
            CreateMap<CreateQuizDto, Quiz>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<CreateChoiceDto, Choice>();
            CreateMap<Question, CreateQuestionDto >().ForMember((dist) => dist.Choices, (src) => src.MapFrom((c) => c.Choices));
            CreateMap<Choice, CreateChoiceDto > ();
        }
    }
}