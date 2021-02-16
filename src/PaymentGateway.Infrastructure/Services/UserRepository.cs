using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure.Services
{
    public class UserRepository : Repository<Login>, IUserRepository
    {
        public UserRepository(UserDbContext context)
        : base(context)
        {
        }

        public UserDbContext UserDbContext
        {
            get { return _context as UserDbContext; }
        }

        public async Task<Login> GetLogin(string username, string password)
        {
            if (( username.Length == 0 || username == null ) && ( password.Length == 0 || password == null ))
            {
                throw new ArgumentNullException("Invalid username or password");
            }

            return await UserDbContext.Logins.Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }
    }
}
