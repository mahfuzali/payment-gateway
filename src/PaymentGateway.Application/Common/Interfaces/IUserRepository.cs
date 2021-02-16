using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<Login>
    {
        Task<Login> GetLogin(string username, string password);
    }
}
