using System;
using System.Threading.Tasks;
using BankSimulator.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeBankController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Withdraw(Card card)
        {
            decimal balance = 1000.00m;
            string status = "successful";

            if (balance - card.Amount < 0)
            {
                status = "declined";
            }

            await Task.Delay(500);

            return Ok(new
            {
                Id = Guid.NewGuid(),
                Status = status
            });
        }
    }
}
