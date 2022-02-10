using AutoMapper;
using bookstoreapp.blazor.server.ui.Services.Base;

namespace bookstoreapp.blazor.server.ui.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadOnlyDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();
        }
    }
}
