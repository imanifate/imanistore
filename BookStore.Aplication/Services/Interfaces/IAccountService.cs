using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels;
using BookStore.Domain.ViewModels.Account;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface IAccountService
    {
       Task<Result> CreatAccountAsync(AccountViewModl model);
        Task<Result> AccountActiveAsync(string activeCode);
        Task<Account?> LoginAsync(LoginViewModel login);
        
    }
}
