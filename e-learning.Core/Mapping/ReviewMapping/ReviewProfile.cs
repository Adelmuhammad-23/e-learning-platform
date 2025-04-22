using AutoMapper;
using e_learning.Core.Features.Review.Commands.Models;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.ReviewMapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<AddReviewCommand, Reviews>();
        }
    }
}
