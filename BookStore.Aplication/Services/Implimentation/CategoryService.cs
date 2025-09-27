using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Contracts;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Category;
using BookStore.Data.Repositores;
using BookStore.Domain.Enums;


namespace BookStore.Aplication.Services.Implimentation
{
    public class CategoryService(IGenericRepository<Category> genericRepository) : ICategoryService
    {
       
        public async Task<Result> CreateAsync(CreateCategoryViewModel model)
        {
            genericRepository.Add(new Category
            {
                ParentId = model.CategoryId,
                Title = model.CategoryTitle
            });

           await genericRepository.SaveAsync();

            return Result.Success; 
        }

        
        public async Task<List<GetCategoryViewModel>> GetAllAsync()
        {
            List<Category> categories = await genericRepository.GetAllAsync();

            if (!categories.Any()) return null;

            return categories
                .Where(c => c.ParentId == null) // فقط ریشه‌ها
                .Select(MapCategoryToViewModelRecursive)
                .ToList();
        }

        private GetCategoryViewModel MapCategoryToViewModelRecursive(Category category)
        {
            return new GetCategoryViewModel()
            {
                CategoryId = category.Id,
                CategoryTitle = category.Title,
                ParentId = category.ParentId,
                IsDeleted = category.IsDelete,
                ChildrenCount = category.Children.Count,
                Childrens = category.Children
                    .Select(MapCategoryToViewModelRecursive)
                    .ToList()
            };
        }

        public async Task<EditCategoryViewModel> GetForEdit(int id)
        {

          Category category =await genericRepository.GetByIdAsync(id);

            return new EditCategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title
            };

        }

        public async Task<Result> Edit(EditCategoryViewModel model)
        {
            Category category = await genericRepository.GetByIdAsync(model.Id);

            if (category == null) return Result.Null;

            category.Title = model.Title;
            category.IsDelete = model.IsDeleted;

            genericRepository.Update(category);
           await genericRepository.SaveAsync();

            return Result.Success;
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
