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
       
        public async Task<CreatResult> CreateAsync(CreateCategoryViewModel model)
        {
            genericRepository.Add(new Category
            {
                ParentId = model.ParentId,
                Title = model.Title,
            });

           await genericRepository.SaveAsync();

            return CreatResult.Success; 
        }

        public async Task<List<ListCategoryViewModel>> GetAllAsync()
        {
           List<Category> categoreis = await genericRepository.GetAllAsync();

            if (categoreis == null) return null;

            return categoreis.Select(c=> new ListCategoryViewModel()
            {
                Title = c.Title,
                IsDelete = c.IsDelete
            }).ToList();
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

        public async Task<EditResult> Edit(EditCategoryViewModel model)
        {
            Category category = await genericRepository.GetByIdAsync(model.Id);

            if (category == null) return EditResult.Null;

            category.Title = model.Title;
            category.IsDelete = model.IsDeleted;

            genericRepository.Update(category);
           await genericRepository.SaveAsync();

            return EditResult.Success;
        }
        public Task<DeleteResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
