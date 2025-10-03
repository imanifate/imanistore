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
    public class BookRepository(BookStoreContext context) : IBookRepository
    {
        public async Task<List<Book>> GetAllByBorrow(int categoryId)
        {
         return await context.Books
             .Include(b => b.borrowings)   // لود امانت‌ها همراه کتاب
             .Where(c => c.CategoryId == categoryId)
             .ToListAsync();

        }

        public List<Book> SerchByTitle(string title)
        {
            return context.Books.Where(b => b.Title == title).ToList();
        }
    }
}
