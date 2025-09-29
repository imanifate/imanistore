
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Account;
using BookStore.Aplication.Security;
using BookStore.Aplication.Utilities;
using BookStore.Data.Migrations;
using BookStore.Domain.ViewModels;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace BookStore.Aplication.Services.Implimentation
{
    public class AccountService(
        IGenericRepository<Account> genericRepository ,
        IAccountRepository accountRepository
        ):IAccountService
    {
        public async Task<Result> CreatAccountAsync(AccountViewModl model)
        {
            if(await accountRepository.ExistEmailAsync(model.Email) ) return Result.EmailDuplicated;
            if(await accountRepository.ExistUserNameAsync(model.UserName)) return Result.UsernaemDuplicated;
            genericRepository.Add(new Account
            {
                UserId = model.UserId,
                Title = model.UserName,
                Email = model.Email,
                PasswordHash = PasswordHasher.EncodePasswordMd5(model.Password),
                ActiveCode = model.ActiveCode
               
            });
            await genericRepository.SaveAsync();
            return Result.Success;
        }

        public async Task<Result> AccountActiveAsync(string activeCode)
        {
           Account account = await accountRepository.GetByActiveCodeAsync(activeCode);
            if (account != null)
            {
                account.IsActive = true;
                account.ActiveCode = CodeGenerators.ActiveCode();
                genericRepository.Update(account);
                await genericRepository.SaveAsync();
            }
            return Result.Success;
            
        }

        public async Task<Account?> LoginAsync(LoginViewModel login)
        {
            if (login.UserNameOrEmail == null || login.Password == null) return null;

          Account account = await accountRepository.LoginAsync(login.UserNameOrEmail, 
              PasswordHasher.EncodePasswordMd5(login.Password));

            if (account != null) return  account;

            return account;
        }

        
    }
}
