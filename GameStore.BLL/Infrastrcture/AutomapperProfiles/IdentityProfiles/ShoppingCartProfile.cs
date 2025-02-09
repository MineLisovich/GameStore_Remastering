using AutoMapper;
using GameStore.DAL.Entities.Identity;
using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture.AutoMapperResolvers.IdentityResolvers.DALtoBLL;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.IdentityProfiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile() 
        {
            CreateMap<ShoppingCart, ShoppingCartDTO>()
                .ForMember(dest=>dest.PaymentDate, opt=>opt.MapFrom(new ShoppingCartDTO_PaymentDate()));

            CreateMap<ShoppingCartDTO, ShoppingCart>();
        }
    }
}
