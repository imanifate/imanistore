using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Contracts
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllByBorrow(int categoryId);
       List<Book> SerchByTitle(string title);

    }
}
