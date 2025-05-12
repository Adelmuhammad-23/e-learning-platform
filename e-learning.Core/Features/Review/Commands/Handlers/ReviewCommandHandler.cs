using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Review.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Data.Entities.Identity;
using e_learning.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Core.Features.Review.Commands.Handlers
{
    public class ReviewCommandHandler : ResponsesHandler,
        IRequestHandler<AddReviewCommand, Responses<string>>,
        IRequestHandler<DeleteReviewCommand, Responses<string>>,
        IRequestHandler<UpdateReviewCommand, Responses<string>>

    {
        private readonly IReviewService _reviewService;
        private readonly ICourseServices _courseServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public ReviewCommandHandler(IReviewService reviewService, ICourseServices courseServices, IAuthenticationServices authenticationServices, UserManager<User> userManager, IMapper mapper)
        {
            _reviewService = reviewService;
            _courseServices = courseServices;
            _authenticationServices = authenticationServices;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Responses<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Reviews>(request);
            var user = _userManager.Users.FirstOrDefault(x => x.Id == request.StudentId);
            if (user == null)
                return NotFound<string>("Student is not found");
            var course = await _courseServices.ExistsAsync(request.CourseId);
            if (!course)
                return NotFound<string>("Course is not found");
            var addReview = await _reviewService.AddReviewAsync(review);
            if (addReview != "Success")
                return BadRequest<string>("Failed to add review");
            return Success("Review added successfully");

        }
        public async Task<Responses<string>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(request.ReviewId);
            if (review == null)
                return NotFound<string>("Review not found");

            review.Rating = request.Rating;
            review.Comment = request.Comment;

            var updated = await _reviewService.UpdateAsync(review);
            return updated
                ? Success("Review updated successfully")
                : BadRequest<string>("Failed to update review");

        }

        public async Task<Responses<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _reviewService.DeleteAsync(request.ReviewId);
            return deleted
                ? Success("Review deleted successfully")
                : NotFound<string>("Review not found");
        }

    }


}
