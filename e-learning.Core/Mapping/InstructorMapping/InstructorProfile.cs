using AutoMapper;
using e_learning.Core.Features.Instructors.Queries.Responses;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.InstructorMapping
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            UpdateInstructorCommandMapping();
            CreateMap<Instructor, InstructorQueryResponse>();

        }
    }
}
