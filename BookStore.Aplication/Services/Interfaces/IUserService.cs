using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.User;

namespace BookStore.Aplication.Services.Implimentation
{
    public interface  IUserService
    {
        Task<CreatResult> RegisterAsync(RegisterUserViewModl model);
    }
}
