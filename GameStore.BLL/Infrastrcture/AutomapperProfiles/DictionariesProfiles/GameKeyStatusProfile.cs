using AutoMapper;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.BLL.DTO.Dictionaries;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.DictionariesProfiles
{
    public class GameKeyStatusProfile : Profile
    {
        public GameKeyStatusProfile() 
        {
            CreateMap<GameKeyStatus,GameKeyStatusDTO>().ReverseMap();
        }
    }
}
