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
        public void Create(Book book)
        {
            _context.Books.Add(book);
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetAllByBorrow()
        {
            return _context.Books.Where(c => c.Borrow == false).ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id==id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Book> SerchByTitle(string title)
        {
          return  _context.Books.Where(b => b.Title == title).ToList();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
