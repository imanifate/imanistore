using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Contracts
{
    public interface IAccountRepository
    {
        Task<bool> ExistEmailAsync(string email);
        Task<bool> ExistUserNameAsync(string userName);
        Task<Account?> GetByActiveCodeAsync(string activeCode);

        Task<Account?> LoginAsync(string userNameoremail, string password);

    }
}
