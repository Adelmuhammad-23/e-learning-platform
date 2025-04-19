using e_learning.Core.Features.Modules.Queries.Responses;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile
    {
        public void GetByCourseIdMapping()
        {
            CreateMap<Module, GetByCourseIdResponse>()
                .ForMember(dest => dest.Videos, src => src.MapFrom(v => v.Videos));

            CreateMap<Video, ListOfVideos>();

        }
    }
}