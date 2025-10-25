using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.Category;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result> CreateAsync(CreateCategoryViewModel model);
       Task<List<GetCategoryViewModel>> GetAllAsync();
      Task<EditCategoryViewModel> GetForEdit(int id);
       Task<Result> Edit(EditCategoryViewModel model);
        Task<Result> Delete(int id);
    }
}
