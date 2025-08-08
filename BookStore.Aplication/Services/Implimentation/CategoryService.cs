using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Enums.Category;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Category;

namespace BookStore.Aplication.Services.Implimentation
{
    public class CategoryService (ICategoryRepository categoryRepository):ICategoryService
    {
        public CreatResult Creat(CreateCategoryViewModel model)
        {
            categoryRepository.Create(new Category
            {
                ParentId = model.ParentId,
                Title = model.Title,
            });

            categoryRepository.Save();

            return CreatResult.Success; 
        }

        public List<ListCategoryViewModel> GetAll()
        {
           List<Category> categoreis = categoryRepository.GetAll();

            if (categoreis == null) return null;

            return categoreis.Select(c=> new ListCategoryViewModel()
            {
                Title = c.Title,
                IsDelete = c.IsDelete
            }).ToList();
        }

        public EditCategoryViewModel GetForEdit(int id)
        {

          Category category = categoryRepository.GetById(id);

            return new EditCategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title
            };

        }

        public EditResult Edit(EditCategoryViewModel model)
        {
            Category category = categoryRepository.GetById(model.Id);

            if (category == null) return EditResult.Null;

            category.Title = model.Title;
            category.IsDelete = model.IsDeleted;

            categoryRepository.Update(category);
            categoryRepository.Save();

            return EditResult.Success;
        }

        public DeleteResult Delete(int id)
        {
            Category category = categoryRepository.GetById(id);

            if (category == null) return DeleteResult.Null;

            category.IsDelete = true;

            categoryRepository.Update(category);
            categoryRepository.Save();

            return DeleteResult.Success;

        }
    }
}
