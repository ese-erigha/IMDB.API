using System;
using System.Linq;
using AutoMapper;
using ResponseDto = IMDB.Api.Models.ResponseDto;

namespace IMDB.Api.Profiles
{
    public class EntityToResponseMappingProfile : Profile
    {
        public EntityToResponseMappingProfile()
        {
            CreateMap<Entities.ContentRating, ResponseDto.ContentRating>();
            CreateMap<Entities.Country, ResponseDto.Country>();
            CreateMap<Entities.Genre, ResponseDto.Genre>();
            CreateMap<Entities.Language, ResponseDto.Language>();
            CreateMap<Entities.User, ResponseDto.User>();
            CreateMap<Entities.User, ResponseDto.AuthUser>();

            CreateMap<Entities.Movie, ResponseDto.Movie>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre)))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.MoviePersons.Select(mg => mg.Person)))
                .ForMember(dest => dest.PlotKeywords, opt => opt.MapFrom(src => src.MoviePlotKeywords.Select(mg => mg.PlotKeyword)));

            CreateMap<Entities.Person, ResponseDto.Person>();
            CreateMap<Entities.PlotKeyword, ResponseDto.PlotKeyword>();
        }
    }
}
