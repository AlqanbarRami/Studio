using AutoMapper;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.User;
using MoviesApi.Resources;

namespace MoviesApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, AdminRegister>();
            CreateMap<FilmStudio, StudioRegister>();
            CreateMap<Film, FilmNUsers>().ReverseMap();
            CreateMap<FilmStudio, StudioResource>().ReverseMap();
        }
    }
}
