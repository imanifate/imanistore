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
        List<Book> GetAll();
        List<Book> GetAllByBorrow();
        void Create(Book book);
        Book GetById(int id);
        void Update(Book book);
        void Save();
       List<Book> SerchByTitle(string title);

    }
}
