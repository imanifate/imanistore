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
    public class BookService(IGenericRepository<Book> genericRepository) : IBookService
    {
        public async Task<CreatResult> CreatAsync(CreateBookViewModel model)
        {
            genericRepository.Add(new Book
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Author = model.Author,
                PublicationDate = model.PublicationDate
            });

           await genericRepository.SaveAsync();

            return CreatResult.Success;
        }

        public async Task<List<GetBookViewModel>>? GetAllAsync()
        {
            List<Book> books =await genericRepository.GetAllAsync();

            if (books == null) return null;

            return books.Select(b => new GetBookViewModel()
            {
                Title = b.Title,
                Author = b.Author,
                PublicationDate = b.PublicationDate,
                IsDeleted = b.IsDelete

            }).ToList();
        }

        //public async Task<List<GetBookViewModel>>? GetAllFreeAsync()
        //{
        //    List<Book> freeBooks =await genericRepository.GetAllByBorrowAsync();
        //    if (freeBooks == null) return null;

        //    return freeBooks.Select(b => new GetBookViewModel()
        //    {
        //        Title = b.Title,
        //        Author = b.Author,
        //        PublicationDate = b.PublicationDate,
        //        IsDeleted = b.IsDelete

        //    }).ToList();
        //}

        //public async Task<List<GetBookViewModel>>? SearchTitleAsync(string title)
        //{
        //    List<Book> books =await genericRepository.SerchByTitleAsync(title);

        //    if (books == null) return null;

        //    return books.Select(b => new GetBookViewModel()
        //    {
        //        Title = b.Title,
        //        Author = b.Author,
        //        PublicationDate = b.PublicationDate,
        //        IsDeleted = b.IsDelete

        //    }).ToList();
        //}

        public async Task<EditBookViewModel>? GetForEditAsync(int id)
        {
            Book book =await genericRepository.GetByIdAsync(id);

            if (book == null) return null;

            return new EditBookViewModel()
            {
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                IsDeleted = book.IsDelete

            };
        }

        public async Task<EditResult> EditAsync(EditBookViewModel model)
        {
            Book book =await genericRepository.GetByIdAsync(model.Id);

            if (book == null) return EditResult.Null;

            book.Title = model.Title;
            book.Author = model.Author;
            book.Borrow = model.Borrow;
            book.IsDelete = model.IsDeleted;

            genericRepository.Update(book);
            await genericRepository.SaveAsync();

            return EditResult.Success;
        }

        public async Task<EditResult> DeleteAsync(int id)
        {
            Book book =await genericRepository.GetByIdAsync(id);

            if(book == null) return EditResult.Null;

            book.IsDelete = true;

            genericRepository.Update(book);
            await genericRepository.SaveAsync();

            return EditResult.Success;
        }

        public Task<List<GetBookViewModel>>? GetAllFreeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBookViewModel>>? SearchTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}
