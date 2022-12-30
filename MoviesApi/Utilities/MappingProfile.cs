using AutoMapper;

namespace MoviesApi.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDetailsDto>().ReverseMap();
            CreateMap<MoviesDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());

        }
    }
}
