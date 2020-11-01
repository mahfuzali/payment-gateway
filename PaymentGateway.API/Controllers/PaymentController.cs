using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Models;
using PaymentGateway.API.Persistence;
using PaymentGateway.API.Services;

namespace PaymentGateway.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository cardRepository, IMapper mapper) {
            _paymentRepository = cardRepository ??
                throw new ArgumentNullException(nameof(cardRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/payment/8b51ea22-adbf-46f3-9f90-cf9d9d534d45
        [HttpGet("{transactionId}", Name = "GetPayment")]
        public async Task<ActionResult<Card>> GetPayment(Guid transactionId)
        {
            var paymentEntity = await _paymentRepository.GetPayment(transactionId);

            if (paymentEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Models.PaymentDto>(paymentEntity));
        }


        // POST: api/payment
        [HttpPost]
        public async Task<ActionResult<Payment>> PostCardPayment(CardDto card)
        {
            var cardEntity = _mapper.Map<Entities.Card>(card);
            var bankResponse = await _paymentRepository.GetBankResponse(cardEntity);

            _paymentRepository.StoreCard(cardEntity);

            Payment paymentEntity = new Payment() { 
                Id = bankResponse.Id,
                Status = bankResponse.Status,
                Card = cardEntity
            };

            _paymentRepository.StorePayment(paymentEntity);

            return Ok(bankResponse);
        }
    }
}
