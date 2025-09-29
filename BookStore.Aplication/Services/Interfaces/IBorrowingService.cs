using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<Result> BorrowBookAsync(int userId, int bookId);
    }
}
