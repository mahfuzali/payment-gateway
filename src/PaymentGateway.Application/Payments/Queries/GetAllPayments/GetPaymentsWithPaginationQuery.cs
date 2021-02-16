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
using PaymentGateway.Application.Common.Helpers;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Mappings;
using PaymentGateway.Application.Common.Models;

namespace PaymentGateway.Application.Payments.Queries.GetPayments
{
    public class GetPaymentsWithPaginationQuery : IRequest<PaginatedList<PaymentDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPaymentsWithPaginationQueryHandler : IRequestHandler<GetPaymentsWithPaginationQuery, PaginatedList<PaymentDto>>
    {
        private readonly IPaymentDbContext context;
        private readonly IMapper mapper;

        public GetPaymentsWithPaginationQueryHandler(IPaymentDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper; 
        }

        public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentsWithPaginationQuery request, CancellationToken cancellationToken) 
            => await this.context.Payments
                    .OrderBy(x => x.Id)
                    .ProjectTo<PaymentDto>(this.mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}
