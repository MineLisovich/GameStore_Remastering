using AutoMapper;
using GameStore.DAL.Entities.History;
using GameStore.BLL.DTO.History;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.HistoryProfiles
{
    public class HistoryGameReviewProfile : Profile
    {
        public HistoryGameReviewProfile()
        {
            CreateMap<HistoryGameReviews, HistoryGameReviewsDTO>().ReverseMap();
        }
    }
}
