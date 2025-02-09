using AutoMapper;
using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture.Singletons;
using GameStore.DAL.Entities.Identity;

namespace GameStore.BLL.Infrastrcture.AutoMapperResolvers.IdentityResolvers.DALtoBLL
{
    public class ShoppingCartDTO_PaymentDate : IValueResolver<ShoppingCart, ShoppingCartDTO, DateTime?>
    {
        public DateTime? Resolve(ShoppingCart source, ShoppingCartDTO dest, DateTime? destMember, ResolutionContext context)
        {
            if (source.PaymentDate.HasValue)
            {
                ConvectorUtcToGmtPlus3Zone toGmt = ConvectorUtcToGmtPlus3Zone.GetInstance();
                return toGmt.ConvectorDate(source.PaymentDate.Value);
            }

            return null;
        }
    }
}
