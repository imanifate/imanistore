using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Domain.Contracts;
using BookStore.Domain.Models;

namespace BookStore.Data.Repositores
{
    public class BookRepository(BookStoreContext _context) : IBookRepository
    {
        public List<Book> GetAllByBorrow()
        {
            return _context.Books.Where(c => c.Borrow == false).ToList();
        }
        public List<Book> SerchByTitle(string title)
        {
          return  _context.Books.Where(b => b.Title == title).ToList();
        }
    }
}
