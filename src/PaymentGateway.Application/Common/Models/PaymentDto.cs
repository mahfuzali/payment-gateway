using System;
using AutoMapper;
using PaymentGateway.Application.Common.Mappings;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Models
{
    public class PaymentDto : IMapFrom<Card>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public CardViewModel Card { get; set; }

        public void Mapping(Profile profile)
            => profile.CreateMap<Domain.Entities.Payment, Models.PaymentDto>()
                .ForMember(
                    dest => dest.Card,
                    opt => opt.MapFrom(src => src.Card)).ReverseMap();
    }
}
