using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Models;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Payments.Commands.CreateAPayment
{
    public class CreateAPaymentCommand : IRequest<BankResponse>
    {
        public CardDto Card { get; set; }
    }

    public class CreateAPaymentCommandHandler : IRequestHandler<CreateAPaymentCommand, BankResponse>
    {
        private readonly IRepositoryWrapper repositoryWrapper;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public CreateAPaymentCommandHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IConfiguration config)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.mapper = mapper;
            this.config = config;
        }

        public async Task<BankResponse> Handle(CreateAPaymentCommand request, CancellationToken cancellationToken)
        {
            var cardEntity = this.mapper.Map<Card>(request.Card);

            var bankEndpoint = this.config["Endpoints:Bank"];

            var bankResponse = await this.repositoryWrapper.PaymentGateways.GetBankResponse(cardEntity, bankEndpoint);

            if (bankResponse == null)
            {
                throw new NotFoundException(nameof(bankResponse), request.Card.Number.Substring(request.Card.Number.Length - 4));
            }

            this.repositoryWrapper.Cards.Add(cardEntity);

            var paymentEntity = new Payment()
            {
                Id = bankResponse.Id,
                Status = bankResponse.Status,
                Card = cardEntity
            };

            this.repositoryWrapper.PaymentGateways.Add(paymentEntity);

            return bankResponse;
        }
    }
}
