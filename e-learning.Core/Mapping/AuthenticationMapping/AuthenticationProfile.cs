using AutoMapper;

namespace e_learning.Core.Mapping.AuthenticationMapping
{
    public partial class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            RegisterCommandMapping();
        }
    }
}
