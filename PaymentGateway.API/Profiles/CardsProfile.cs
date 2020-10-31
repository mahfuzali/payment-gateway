using AutoMapper;
using PaymentGateway.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Profiles
{
    public class CardsProfile : Profile
    {
        public CardsProfile() {

            CreateMap<Entities.Card, Models.CardDto>()
                .ForMember(
                    dest => dest.Expiry,
                    opt => opt.MapFrom(src => $"{src.ExpiryMonth}/{src.ExpiryYear}"));

            CreateMap<Models.CardDto, Entities.Card>()
                .ForMember(
                    dest => dest.ExpiryMonth,
                    opt => opt.MapFrom(src => src.Expiry.ExpiryDateArray()[0]))
                .ForMember(
                    dest => dest.ExpiryYear,
                    opt => opt.MapFrom(src => src.Expiry.ExpiryDateArray()[1]));
        }
    }
}
