using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Book;

namespace BookStore.Aplication.Services.Implimentation
{
    public class BookService
        (IGenericRepository<Book> genericRepository,
        IBookRepository bookRepository) : IBookService
    {
        public async Task<Result> CreatAsync(CreateBookViewModel model)
        {
            genericRepository.Add(new Book
            {
                CategoryId = model.CategoryId,
                Title = model.BookTitle,
                Author = model.Author,
                PublicationDate = model.PublicationDate,
                Publisher = model.Publisher
                
            });

           await genericRepository.SaveAsync();

            return Result.Success;
        }
       
        public async Task<ListBookViewModel>? GetAllAsync(int categoryId)
        {
            List<Book> Allbooks = await bookRepository.GetAllByBorrowAsync(categoryId);
           
            if (Allbooks == null) return null;
            var model = new ListBookViewModel
            {
                CategoryId = categoryId,
                Books = Allbooks.Select(b => new GetBookViewModel()
                {                   
                    CategoryId = b.CategoryId,
                    BookId = b.Id,
                    BookTitle = b.Title,
                    Author = b.Author,
                    PublicationDate = b.PublicationDate,
                    Publisher = b.Publisher,
                    IsDeleted = b.IsDelete,
                    Borrow = b.borrowings.Any(br => !br.IsReturn)

                }).ToList()
            };
            return model;
            }


        public async Task<ListBookViewModel>? SearchTitleAsync(string title)
        {
            List<Book> books = await bookRepository.SearchByBookAndAuthorAsync(title);

            if (books == null) return null;

            var model = new ListBookViewModel
            {
                
                Books = books.Select(b => new GetBookViewModel()
                {

                    CategoryId = b.CategoryId,
                    BookId = b.Id,
                    BookTitle = b.Title,
                    Author = b.Author,
                    PublicationDate = b.PublicationDate,
                    Publisher = b.Publisher,
                    IsDeleted = b.IsDelete,
                    Borrow = b.borrowings.Any(br => !br.IsReturn)

                }).ToList()
            };
            return model;
        }
        public async Task<ListBookViewModel>? SearchAuthorAsync(string title)
        {
            List<Book> books = await bookRepository.SearchByAuthorAsync(title);

            if (books == null) return null;

            var model = new ListBookViewModel
            {

                Books = books.Select(b => new GetBookViewModel()
                {

                    CategoryId = b.CategoryId,
                    BookId = b.Id,
                    BookTitle = b.Title,
                    Author = b.Author,
                    PublicationDate = b.PublicationDate,
                    Publisher = b.Publisher,
                    IsDeleted = b.IsDelete,
                    Borrow = b.borrowings.Any(br => !br.IsReturn)

                }).ToList()
            };
            return model;
        }

        public async Task<EditBookViewModel>? GetForEditAsync(int id)
        {
            Book book =await genericRepository.GetByIdAsync(id);

            if (book == null) return null;

            return new EditBookViewModel()
            {

                BookId = book.Id,
                CategoryId = book.CategoryId,
                BookTitle = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                PublicationDate = book.PublicationDate,
                IsDeleted = book.IsDelete, 
                Borrow = book.borrowings != null && book.borrowings.Any(b => b.ReturnDate == null)

            };
        }

        public async Task<Result> EditAsync(EditBookViewModel model)
        {
            Book book =await genericRepository.GetByIdAsync(model.BookId);

            if (book == null) return Result.Null;

            book.Title = model.BookTitle;
            book.Author = model.Author;
           book.Publisher = model.Publisher;
            book.PublicationDate = model.PublicationDate;
            book.IsDelete = model.IsDeleted;
            
            genericRepository.Update(book);
            await genericRepository.SaveAsync();

            return Result.Success;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            Book book =await genericRepository.GetByIdAsync(id);

            if(book == null) return Result.Null;

            book.IsDelete = true;

            genericRepository.Update(book);
            await genericRepository.SaveAsync();

            return Result.Success;
        }
        public async Task<Result> UnDeleteAsync(int id)
        {
            Book book = await genericRepository.GetByIdAsync(id);

            if (book == null) return Result.Null;

            book.IsDelete = false;

            genericRepository.Update(book);
            await genericRepository.SaveAsync();

            return Result.Success;
        }

    }
}
