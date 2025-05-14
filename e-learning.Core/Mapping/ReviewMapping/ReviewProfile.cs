using AutoMapper;
using e_learning.Core.Features.Review.Commands.Models;
using e_learning.Core.Features.Review.Queries.Responses;

using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.ReviewMapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<AddReviewCommand, Reviews>();
            CreateMap<Reviews, GetReviewsByCourseIdResponse>()
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Student.Image));
            CreateMap<Student, GetReviewsByCourseIdResponse>()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<Reviews, GetReviewByIdResponse>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name));


        }
    }
}
