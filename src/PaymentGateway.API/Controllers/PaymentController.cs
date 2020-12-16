using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;

        public PaymentController(IRepositoryWrapper repositoryWrapper, IMapper mapper,
                                    ILogger<PaymentController> logger, IConfiguration config)
        {
            _repositoryWrapper = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _config = config ??
                throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Retrieving a payment's details.
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
        /// <response code="404">If the payment is null</response>  
        [HttpGet("{transactionId}", Name = "GetPayment")]
        public async Task<IActionResult> GetPayment(Guid transactionId)
        {
            _logger.LogInformation("Retrieving previous transation with id {transactionId}", transactionId);

            var paymentEntity = await _repositoryWrapper.PaymentGateways.GetPayment(transactionId);

            if (paymentEntity == null)
            {
                _logger.LogWarning("GetPayment({transactionId}) NOT FOUND", transactionId);
                return NotFound();
            }

            return Ok(_mapper.Map<Application.Models.PaymentDto>(paymentEntity));
        }

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
        public async Task<IActionResult> PostCardPayment(CardDto card)
        {
            var cardEntity = _mapper.Map<Domain.Entities.Card>(card);

            string cardLastDigits = card.Number.Substring(card.Number.Length - 4);
            _logger.LogInformation("Making a payment with card ending with {cardLastDigits}", cardLastDigits);

            var bankEndpoint = _config.GetValue<string>("Endpoints:Bank");

            var bankResponse = await _repositoryWrapper.PaymentGateways.GetBankResponse(cardEntity, bankEndpoint);

            if (bankResponse == null)
            {
                _logger.LogWarning("Did not receive a response from the bank with card ending with {cardLastDigits}", cardLastDigits);
                return NotFound();
            }

            _repositoryWrapper.Cards.Add(cardEntity);

            Payment paymentEntity = new Payment()
            {
                Id = bankResponse.Id,
                Status = bankResponse.Status,
                Card = cardEntity
            };

            _repositoryWrapper.PaymentGateways.Add(paymentEntity);

            return Ok(bankResponse);
        }
    }
}
