using AutoMapper;
using GameStore.DAL.Entities.Games;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture.AutoMapperResolvers.GameResolvers.DALtoBLL;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.GamesProfiles
{
    public class GameReviewProfile : Profile
    {
        public GameReviewProfile()
        {
            CreateMap<GameReview, GameReviewDTO>()
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(new GameReviewDTO_DateCreateResolver()))
                .ForMember(dest => dest.DateModified, opt => opt.MapFrom(new GameReviewDTO_DateModifiedResolver()));
            CreateMap<GameReviewDTO, GameReview>();
        }
    }
}
