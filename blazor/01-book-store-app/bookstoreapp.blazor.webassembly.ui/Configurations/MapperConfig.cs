using AutoMapper;
using bookstoreapp.blazor.webassembly.ui.Services.Base;

namespace bookstoreapp.blazor.webassembly.ui.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadOnlyDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<BookDetailsDto, BookUpdateDto>().ReverseMap();
        }
    }
}
