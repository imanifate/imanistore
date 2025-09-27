using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.Book;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface IBookService
    {
        Task<Result> CreatAsync(CreateBookViewModel model);
        Task<ListBookViewModel>? GetAllAsync(int categoryId);
        Task<List<GetBookViewModel>>? GetAllFreeAsync();
        Task<List<GetBookViewModel>>? SearchTitleAsync(string title);
        Task<EditBookViewModel>? GetForEditAsync(int id);
        Task<Result> EditAsync(EditBookViewModel model);
        Task<Result> DeleteAsync(int id);
    }
}
