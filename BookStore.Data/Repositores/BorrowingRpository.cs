using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Domain.Contracts;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositores
{
    public class BorrowingRpository(BookStoreContext context):IBorrowingRpository
    {
        public async Task<Borrowing> GetByBookAndUser(int userId, int bookId)
        {
          return await context.Borrowing.FirstOrDefaultAsync(b => b.BookId == bookId && b.UserId == userId && b.IsReturn == false);
        }
    }
}
