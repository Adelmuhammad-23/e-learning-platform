using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Review.Queries.Models;
using e_learning.Core.Features.Review.Queries.Responses;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Review.Queries.Handlers
{
    public class ReviewQueryHandler : ResponsesHandler,
        IRequestHandler<GetReviewsByCourseIdQuery, Responses<List<GetReviewsByCourseIdResponse>>>,
        IRequestHandler<GetReviewByIdQuery, Responses<GetReviewByIdResponse>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewQueryHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<Responses<List<GetReviewsByCourseIdResponse>>> Handle(GetReviewsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetReviewsByCourseAsync(request.CourseId);
            if (reviews == null || !reviews.Any())
                return NotFound<List<GetReviewsByCourseIdResponse>>("No reviews found for this course");

            var mapped = _mapper.Map<List<GetReviewsByCourseIdResponse>>(reviews);
            return Success(mapped);
        }

        public async Task<Responses<GetReviewByIdResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(request.ReviewId);
            return review == null
                ? NotFound<GetReviewByIdResponse>("Review not found")
                : Success(_mapper.Map<GetReviewByIdResponse>(review));
        }

    }
}