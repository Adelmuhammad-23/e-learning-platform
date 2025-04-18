using e_learning.Core.Features.Modules.Commands.Models;
using e_learning.Data.Entities;

namespace e_learning.Core.Mapping.ModuleMapping.CommandMapping
{
    public partial class ModuleProfile
    {
        public void AddModuleMapping()
        {
            CreateMap<AddModuleCommand, Module>();
            CreateMap<UpdateModuleCommand, Module>();

        }
    }
}
