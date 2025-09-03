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
        Task<CreatResult> CreatAsync(CreateBookViewModel model);
        Task<List<GetBookViewModel>>? GetAllAsync();
        Task<List<GetBookViewModel>>? GetAllFreeAsync();
        Task<List<GetBookViewModel>>? SearchTitleAsync(string title);
        Task<EditBookViewModel>? GetForEditAsync(int id);
        Task<EditResult> EditAsync(EditBookViewModel model);
        Task<EditResult> DeleteAsync(int id);
    }
}
