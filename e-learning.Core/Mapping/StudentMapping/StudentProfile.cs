using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Core.Mapping.StudentMapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<UpdateStudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
        }
    }
}
