using AutoMapper;

namespace e_learning.Core.Mapping.VideosMapping
{
    public partial class VideoProfile : Profile
    {
        public VideoProfile()
        {
            AddVideoInCourseMapping();

        }
    }
}
