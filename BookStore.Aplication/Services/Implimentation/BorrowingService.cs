using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Data.Repositores;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;

namespace BookStore.Aplication.Services.Implimentation
{
    public class BorrowingService (
        IGenericRepository<Borrowing> genericRepository,
        IBorrowingRpository borrowingRepository
        ): IBorrowingService
    {
        public async Task<Result> BorrowBookAsync(int userId, int bookId)
        {
            
            genericRepository.Add(new Borrowing
            {
                UserId = userId,
                BookId = bookId,
                IsReturn = false
            });
            await genericRepository.SaveAsync();
            return Result.Success;
        }
        public async Task<Result> ReturnBookAsync(int userId, int bookId)
        {
            Borrowing borrowing =await  borrowingRepository.GetByBookAndUser(userId, bookId);
            if (borrowing == null) return Result.Null;

            borrowing.UserId = userId;
            borrowing.BookId = bookId;
            borrowing.IsReturn = true;
            borrowing.ReturnDate = DateTime.Now;

            genericRepository.Update(borrowing);
            await genericRepository.SaveAsync();

            return Result.Success;
        }
    }
}
