using e_learning.Core.Features.Authentication.Commands.Models;
using e_learning.Data.Entities.Identity;

namespace e_learning.Core.Mapping.AuthenticationMapping
{
    public partial class AuthenticationProfile
    {
        public void RegisterCommandMapping()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
