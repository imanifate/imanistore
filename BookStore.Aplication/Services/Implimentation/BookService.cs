using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums.Book;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Book;

namespace BookStore.Aplication.Services.Implimentation
{
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        public CreatResult Creat(CreateBookViewModel model)
        {
            bookRepository.Create(new Book
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Author = model.Author,
                PublicationDate = model.PublicationDate
            });

            bookRepository.Save();

            return CreatResult.Success;
        }

        public List<GetBookViewModel>? GetAll()
        {
            List<Book> books = bookRepository.GetAll();

            if (books == null) return null;

            return books.Select(b => new GetBookViewModel()
            {
                Title = b.Title,
                Author = b.Author,
                PublicationDate = b.PublicationDate,
                IsDeleted = b.IsDelete

            }).ToList();
        }

        public List<GetBookViewModel>? GetAllFree()
        {
            List<Book> freeBooks = bookRepository.GetAllByBorrow();
            if (freeBooks == null) return null;

            return freeBooks.Select(b => new GetBookViewModel()
            {
                Title = b.Title,
                Author = b.Author,
                PublicationDate = b.PublicationDate,
                IsDeleted = b.IsDelete

            }).ToList();
        }

        public List<GetBookViewModel>? SearchTitle(string title)
        {
            List<Book> books = bookRepository.SerchByTitle(title);

            if (books == null) return null;

            return books.Select(b => new GetBookViewModel()
            {
                Title = b.Title,
                Author = b.Author,
                PublicationDate = b.PublicationDate,
                IsDeleted = b.IsDelete

            }).ToList();
        }

        public EditBookViewModel? GetForEdit(int id)
        {
            Book book = bookRepository.GetById(id);

            if (book == null) return null;

            return new EditBookViewModel()
            {
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                IsDeleted = book.IsDelete

            };
        }

        public EditResult Edit(EditBookViewModel model)
        {
            Book book = bookRepository.GetById(model.Id);

            if (book == null) return EditResult.Null;

            book.Title = model.Title;
            book.Author = model.Author;
            book.Borrow = model.Borrow;
            book.IsDelete = model.IsDeleted;
            
            bookRepository.Update(book);
            bookRepository.Save();

            return EditResult.Success;
        }

        public EditResult Delete(int id)
        {
            Book book = bookRepository.GetById(id);

            if(book == null) return EditResult.Null;

            book.IsDelete = true;

            bookRepository.Update(book);
            bookRepository.Save();

            return EditResult.Success;
        }
    }
}
