using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums.Category;
using BookStore.Domain.ViewModels.Category;

namespace BookStore.Aplication.Services.Interfaces
{
    public interface ICategoryService
    {
        CreatResult Creat(CreateCategoryViewModel model);
       List<ListCategoryViewModel> GetAll();
        EditCategoryViewModel GetForEdit(int id);
        EditResult Edit(EditCategoryViewModel model);
        DeleteResult Delete(int id);
    }
}
