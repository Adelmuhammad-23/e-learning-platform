using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Core.Mapping.EnrollmentMapping
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentDTO>();
        }
    }
}
