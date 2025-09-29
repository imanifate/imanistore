using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.User;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface  IUserService
    {
        Task<RegisterResultViewModl> RegisterAsync(RegisterUserViewModl model);
    }
}
