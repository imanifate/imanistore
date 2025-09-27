using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Domain.Contracts
{
    public class AccountRepository(BookStoreContext context) : IAccountRepository
    {
        public async Task<bool> ExistEmailAsync(string email)
        {
            return await context.Accounts.AnyAsync(a => a.Email == email);
        }

        public async Task<bool> ExistUserNameAsync(string userName)
        {
            return await context.Accounts.AnyAsync(a => a.Title == userName);
        }

        public async Task<Account?> GetByActiveCodeAsync(string activeCode)
        {
            return await context.Accounts.FirstOrDefaultAsync(a => a.ActiveCode == activeCode);
        }
        public async Task<Account?> LoginAsync(string userNameoremail, string password)
        {
            var account = context.Accounts.SingleOrDefaultAsync
                (u => (u.Title == userNameoremail || u.Email == userNameoremail)
                && u.PasswordHash == password && u.IsActive == true && u.IsDelete == false);
            return await account;
        }


    }
}
