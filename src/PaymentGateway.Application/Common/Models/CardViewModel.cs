using AutoMapper;

namespace PaymentGateway.Application.Common.Models
{
    public class CardViewModel : CardDto
    {
        public override void Mapping(Profile profile) 
            => profile.CreateMap<Domain.Entities.Card, Models.CardViewModel>()
                .ForMember(
                    dest => dest.Number,
                    opt => opt.MapFrom(src => $"**********{src.Number.Substring(src.Number.Length - 4)}"))
                .ForMember(
                    dest => dest.Expiry,
                    opt => opt.MapFrom(src => $"**/{src.ExpiryYear}"))
                .ForMember(
                    dest => dest.CVV,
                    opt => opt.MapFrom(src => $"***")
                );
    }
}
