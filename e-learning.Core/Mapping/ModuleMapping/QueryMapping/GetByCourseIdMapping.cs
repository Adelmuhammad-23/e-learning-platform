using e_learning.Core.Features.Modules.Queries.Responses;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile
    {
        public void GetByCourseIdMapping()
        {
            CreateMap<Module, GetByCourseIdResponse>()
                .ForMember(dest => dest.Videos, src => src.MapFrom(v => v.Videos))
                .ForMember(dest => dest.Quizzes, src => src.MapFrom(v => v.Quizzes));

            CreateMap<Quiz, ListOfQuizzesDto>();
            CreateMap<Video, ListOfVideos>();

        }
    }
}