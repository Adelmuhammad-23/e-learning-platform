using e_learning.Core.Features.Modules.Commands.Models;
using e_learning.Core.Features.Videos.Commands.Models;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.VideosMapping
{
    public partial class VideoProfile
    {
        public void AddVideoInCourseMapping()
        {
            CreateMap<AddVideoInCourse, Video>()
                .ForMember(dest => dest.Url, src => src.MapFrom(v => v.videoFile));

            CreateMap<AddVideoToModuleCommand, Video>()
               .ForMember(dest => dest.Url, src => src.MapFrom(v => v.VideoFile));

        }
    }
}
