using System;
using AutoMapper;
using ViewModel = IMDB.Api.Models.ViewModel;
namespace IMDB.Api.Profiles
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ViewModel.ContentRating, Entities.ContentRating>();
            CreateMap<ViewModel.Country, Entities.Country>();
            CreateMap<ViewModel.Genre, Entities.Genre>();
            CreateMap<ViewModel.Language, Entities.Language>();
            CreateMap<ViewModel.PlotKeyword, Entities.PlotKeyword>();
            CreateMap<ViewModel.Person, Entities.Person>();
            CreateMap<ViewModel.User, Entities.User>();
            CreateMap<ViewModel.Movie, Entities.Movie>()
                        .ForSourceMember(src => src.Director, opt => opt.Ignore())
                        .ForSourceMember(src => src.ContentRating, opt => opt.Ignore())
                        .ForSourceMember(src => src.Country, opt => opt.Ignore())
                        .ForSourceMember(src => src.Language, opt => opt.Ignore())
                        .ForSourceMember(src => src.Actors, opt => opt.Ignore())
                        .ForSourceMember(src => src.Genres, opt => opt.Ignore())
                        .ForSourceMember(src => src.PlotKeywords, opt => opt.Ignore())
                        .ForMember(dest => dest.DirectorId, opt => opt.MapFrom(src => src.Director))
                        .ForMember(dest => dest.ContentRatingId, opt => opt.MapFrom(src => src.ContentRating))
                        .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.Language))
                        .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Country))
                        .ForMember(dest => dest.Director, opt => opt.Ignore())
                        .ForMember(dest => dest.Country, opt => opt.Ignore())
                        .ForMember(dest => dest.Language, opt => opt.Ignore())
                        .ForMember(dest => dest.ContentRating, opt => opt.Ignore())
                        .ForMember(dest => dest.MovieGenres, opt => opt.Ignore())
                        .ForMember(dest => dest.MoviePersons, opt => opt.Ignore())
                        .ForMember(dest => dest.MoviePlotKeywords, opt => opt.Ignore());
        }
    }
}
