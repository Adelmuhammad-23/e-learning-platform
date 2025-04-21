using AutoMapper;

namespace e_learning.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile : Profile
    {
        public ModuleProfile()
        {

            AddModuleMapping();
            GetByCourseIdMapping();
        }
    }
}
