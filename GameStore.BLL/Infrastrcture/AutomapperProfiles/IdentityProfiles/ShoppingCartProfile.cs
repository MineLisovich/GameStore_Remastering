using AutoMapper;
using GameStore.DAL.Entities.Identity;
using GameStore.BLL.DTO.Identity;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.IdentityProfiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile() 
        {
            CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();
        }
    }
}
