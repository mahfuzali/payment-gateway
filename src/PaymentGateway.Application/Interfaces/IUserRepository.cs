using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Login> GetLogin(string username, string password);
    }
}
