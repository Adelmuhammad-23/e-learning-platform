using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Core.Mapping.QuizMapping
{
    internal class QuizeProfileProfile : Profile
    {
        public QuizeProfileProfile()
        {
            CreateMap<Quiz, QuizWithQuestionsDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<CreateQuizDto, Quiz>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<CreateChoiceDto, Choice>();
        }
    }
}