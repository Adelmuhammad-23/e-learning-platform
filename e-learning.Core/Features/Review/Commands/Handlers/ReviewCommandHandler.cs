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
        IRequestHandler<AddReviewCommand, Responses<string>>
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
            var token = await _authenticationServices.ValidateToken(request.Token);
            switch (token)
            {
                case "InvalidToken":
                    return Unauthorized<string>("Token is not valid");
                case "NotExpired":
                    {
                        var addReview = await _reviewService.AddReviewAsync(review);
                        if (addReview != "Success")
                            return BadRequest<string>("Failed to add review");
                        return Success("Review added successfully");
                    }
                default:
                    return BadRequest<string>("error when check token is valid or not");
            }
        }
    }
}
