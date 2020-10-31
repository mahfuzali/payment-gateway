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
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardController(ICardRepository cardRepository, IMapper mapper) {
            _cardRepository = cardRepository ??
                throw new ArgumentNullException(nameof(cardRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        // GET: api/card
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Card>>> GetPayments()
        //{
        //    var cardRepo = _cardRepository.GetAllCardPayments();
        //    return await _context.Cards.ToListAsync();
        //}

        // GET: api/card/5
        [HttpGet]
        [Route("{number}", Name = "GetPayment")]
        public async Task<ActionResult<Card>> GetPayment(string number)
        {
            var cardEntity = await _cardRepository.GetPayment(number);

            if (cardEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Models.CardDto>(cardEntity));
        }

        // POST: api/card
        [HttpPost]
        public async Task<ActionResult<Card>> PostCardPayment(CardDto card)
        {
            var cardEntity = _mapper.Map<Entities.Card>(card);
            _cardRepository.MakeCardPayment(cardEntity);
            await _cardRepository.SaveChangesAsync();

            return CreatedAtRoute("GetPayment", 
                new { number = cardEntity.Number }, 
                cardEntity);
        }
    }
}
