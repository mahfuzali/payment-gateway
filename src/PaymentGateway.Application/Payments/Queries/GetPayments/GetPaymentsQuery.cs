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

namespace PaymentGateway.Application.Payments.Queries.GetPayments
{
    public class GetPaymentsQuery: IRequest<IEnumerable<PaymentDto>>
    {
    }

    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentDbContext context;
        private readonly IMapper mapper;

        public GetPaymentsQueryHandler(IPaymentDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper; 
        }

        public async Task<IEnumerable<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var response = await this.context.Payments
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .ProjectTo<PaymentDto>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            return response;
        }   

    }
}
