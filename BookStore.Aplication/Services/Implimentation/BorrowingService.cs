using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;

namespace BookStore.Aplication.Services.Implimentation
{
    public class BorrowingService (IGenericRepository<Borrowing> genericRepository): IBorrowingService
    {
        public async Task<Result> BorrowBookAsync(int userId, int bookId)
        {
            
            genericRepository.Add(new Borrowing
            {
                UserId = userId,
                BookId = bookId,
                IsReturn = false,
                ReturnDate = DateTime.Now
            });
            await genericRepository.SaveAsync();
            return Result.Success;
        }
    }
}
