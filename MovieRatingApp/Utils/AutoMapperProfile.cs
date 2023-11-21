using AutoMapper;
using MovieRatingApp.Models.DbModels;
using MovieRatingApp.Models.ViewDTOs;

namespace MovieRatingApp.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MainPageMovie>();
        }
    }
}
