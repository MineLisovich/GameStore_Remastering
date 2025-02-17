using AutoMapper;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture.Singletons;
using GameStore.DAL.Entities.Games;

namespace GameStore.BLL.Infrastrcture.AutoMapperResolvers.GameResolvers.DALtoBLL
{
    public class GameReviewDTO_DateCreateResolver : IValueResolver<GameReview, GameReviewDTO, DateTime>
    {
        public DateTime Resolve(GameReview source, GameReviewDTO dest, DateTime destMember, ResolutionContext context)
        {
            ConvectorUtcToGmtPlus3Zone toGmt = ConvectorUtcToGmtPlus3Zone.GetInstance();
            return toGmt.ConvectorDate(source.DateCreate);
        }
    }
}
