using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Common.Helpers;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Models;
using PaymentGateway.Application.Payments.Commands.CreateAPayment;
using PaymentGateway.Application.Payments.Queries.GetPayment;
using PaymentGateway.Application.Payments.Queries.GetPayments;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController : ApiControllerBase
    {
        /// <summary>
        /// Retrieves a single payment's details.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET  /api/payment/8b51ea22-adbf-46f3-9f90-cf9d9d534d45
        ///
        /// </remarks>
        /// <param name="transactionId"></param>
        /// <returns>Retrievs a previous payment</returns>
        /// <response code="200">Returns the stored payment</response>
        /// <response code="404">If the payment not found</response>
        [HttpGet("{transactionId}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(Guid transactionId) =>
            await this.Mediator.Send(new GetAPaymentQuery { TransactionId = transactionId});


        /// <summary>
        /// Retrieves all payments details.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET  /api/payment
        ///
        /// </remarks>
        /// <param name="query"></param>
        /// <returns>Retrievs a previous payment</returns>
        /// <response code="200">Returns all stored payments</response>
        /// <response code="404">If there is not payment history</response>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<PaymentDto>>> GetAllPayments([FromQuery] GetPaymentsWithPaginationQuery query) 
            => await this.Mediator.Send(query);


        /// <summary>
        /// Process a payment.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/payment
        ///     {
        ///        "number": "9853425745812345",
        ///        "expiry": "01/2025",
        ///        "amount": 150.50,
        ///        "currency": "USD",
        ///        "cvv": "578"
        ///     }
        ///
        /// </remarks>
        /// <param name="card"></param>
        /// <returns>Returns the bank's response containing unique identifier and a status indicating payment was successful or not</returns>
        /// <response code="200">Returns the bank response with id and status</response>
        /// <response code="404">If the bank response is null</response>
        [HttpPost]
        public async Task<ActionResult<BankResponse>> PostCardPayment(CardDto card) =>
            await this.Mediator.Send(new CreateAPaymentCommand { Card = card });
    }
}
