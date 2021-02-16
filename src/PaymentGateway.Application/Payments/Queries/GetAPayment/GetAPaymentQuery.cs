using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Models;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Payments.Queries.GetPayment
{
    public class GetAPaymentQuery : IRequest<PaymentDto>
    {
        public Guid TransactionId { get; set; }
    }

    public class GetAPaymentQueryHandle : IRequestHandler<GetAPaymentQuery, PaymentDto>
    {
        private readonly IPaymentDbContext context;
        private readonly IMapper mapper;

        public GetAPaymentQueryHandle(IPaymentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaymentDto> Handle(GetAPaymentQuery request, CancellationToken cancellationToken) 
            => await this.context.Payments
                        .Where(x => x.Id == request.TransactionId)
                        .ProjectTo<PaymentDto>(this.mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken);
    }
}
