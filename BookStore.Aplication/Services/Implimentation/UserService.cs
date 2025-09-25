using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.User;

namespace BookStore.Aplication.Services.Implimentation
{
    public class UserService (IGenericRepository<User> genericRepository ): IUserService
    {
        public async Task<CreatResult> RegisterAsync(RegisterUserViewModl model)
        {
            genericRepository.Add(new User
            {
                Title = model.FullName,
                DateAt = DateTime.Now,
                IsDelete = false,
                NationalCode = model.NationalCode,
                PhoneNumber = model.PhoneNumber
               
            });
            genericRepository.SaveAsync();
            return CreatResult.Success;
        }
    }
}
