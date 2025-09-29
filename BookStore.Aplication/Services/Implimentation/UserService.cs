using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.User;

namespace BookStore.Aplication.Services.Implimentation
{
    public class UserService (IGenericRepository<User> genericRepository ): IUserService
    {
        public async Task<RegisterResultViewModl> RegisterAsync(RegisterUserViewModl model)
        {
            User user = new User
            {
                Title = model.FullName,
                DateAt = DateTime.Now,
                IsDelete = false,
                NationalCode = model.NationalCode,
                PhoneNumber = model.PhoneNumber

            };
                   genericRepository.Add(user);
            await  genericRepository.SaveAsync();

            return new RegisterResultViewModl
            {
                Status = Result.Success,
                UserId = user.Id
            };
        }
            
    }
}
