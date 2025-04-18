using AutoMapper;

namespace e_learning.Core.Mapping.ModuleMapping.CommandMapping
{
    public partial class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            AddModuleMapping();
        }
    }
}
