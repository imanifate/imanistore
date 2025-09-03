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
        Task<CreatResult> CreateAsync(CreateCategoryViewModel model);
       Task<List<ListCategoryViewModel>> GetAllAsync();
      Task<EditCategoryViewModel> GetForEdit(int id);
       Task<EditResult> Edit(EditCategoryViewModel model);
        Task<DeleteResult> Delete(int id);
    }
}
