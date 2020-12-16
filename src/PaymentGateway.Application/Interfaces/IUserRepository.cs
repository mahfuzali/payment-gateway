using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Interfaces
{
    public interface IUserRepository : IRepository<Login>
    {
        Task<Login> GetLogin(string username, string password);
    }
}
