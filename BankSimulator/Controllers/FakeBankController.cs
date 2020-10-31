using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeBankController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Withdraw(string number, int expiryMonth, int expiryYear, double amount, string currency, int cvv)
        {
            double balance = 1000;
            string status = "successful";

            if (balance - amount < 0) {
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
