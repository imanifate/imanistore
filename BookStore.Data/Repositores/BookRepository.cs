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
        public async Task<List<Book>> GetAllByBorrowAsync(int categoryId)
        {
         return await context.Books
             .Include(b => b.borrowings)   // لود امانت‌ها همراه کتاب
             .Where(c => c.CategoryId == categoryId)
             .ToListAsync();

        }
        public async Task<List<Book>> SearchByBookAndAuthorAsync(string title)
        {
            return await context.Books.Where(b => b.Title.Contains(title) || b.Author.Contains(title)).ToListAsync();
        }
        public async Task<List<Book>> SearchByAuthorAsync(string author)
        {
            return await context.Books
                .Where(b => b.Author.Contains(author))
                .Include(b => b.borrowings)
                .ToListAsync();
        }

    }
}
