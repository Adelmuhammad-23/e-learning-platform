using e_learning.Core.Features.Instructors.Commands.Models;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.InstructorMapping
{
    public partial class InstructorProfile
    {
        public void UpdateInstructorCommandMapping()
        {
            CreateMap<UpdateInstructorCommand, Instructor>()
                .ForMember(dest => dest.Image, src => src.MapFrom(i => i.Image));
            CreateMap<AddProfessionalInstructorCommand, Instructor>()
                .ForMember(dest => dest.Certificates, src => src.MapFrom(i => i.Certificates));

        }
    }
}
