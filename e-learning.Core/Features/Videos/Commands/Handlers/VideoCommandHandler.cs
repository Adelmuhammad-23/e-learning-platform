using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Videos.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Videos.Commands.Handlers
{
    public class VideoCommandHandler : ResponsesHandler,
        IRequestHandler<AddVideoInCourse, Responses<string>>
    {
        #region
        private readonly IVideoServices _videoServices;
        private readonly IMapper _mapper;

        #endregion
        #region constructors
        public VideoCommandHandler(IVideoServices videoServices, IMapper mapper)
        {
            _videoServices = videoServices;
            _mapper = mapper;

        }
        #endregion
        #region Handel Functions
        public async Task<Responses<string>> Handle(AddVideoInCourse request, CancellationToken cancellationToken)
        {
            var videoMapping = _mapper.Map<Video>(request);
            var result = await _videoServices.AddVideoAsync(videoMapping, request.videoFile);
            if (result != null)
                return Success($"Add this video in course with ID:{request.CourseId} is successfuly");
            return BadRequest<string>(" Failed to add this video");
        }
        #endregion

    }
}
