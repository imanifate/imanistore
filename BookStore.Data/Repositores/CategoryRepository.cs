using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Domain.Contracts;
using BookStore.Domain.Models;
using Microsoft.Identity.Client;

namespace BookStore.Data.Repositores
{
    public class CategoryRepository(BookStoreContext _contexte) : ICategoryRepository
    {
        public void Save()
        {
            _contexte.SaveChanges();
        }
        public void Create(Category category)
        {
            _contexte.Categories.Add(category);
        }

        public List<Category> GetAll()
        {
            List<Category> categoreis = _contexte.Categories
                .Where(c => c.ParentId == null)
                .ToList();

            foreach (var category in categoreis)
            {
                LoadChildrenRecursive(category);
            }

            return categoreis;
        }

        private void LoadChildrenRecursive(Category category)
        {
            var children = _contexte.Categories
                .Where(c => c.ParentId == category.Id)
                .ToList ();

            category.Children = children;

            foreach (var child in children)
            {
                LoadChildrenRecursive (child);
            }
        }

        public Category? GetById(int id)
        {
            Category category = _contexte.Categories.FirstOrDefault(c => c.Id == id);

            if(category == null) return null;

            return category;
        }
        public void Update(Category category)
        {
            _contexte.Categories.Update(category);
        }

       
    }
}
