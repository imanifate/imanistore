using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Domain.Contracts;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositores
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntite
    {
        protected readonly BookStoreContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BookStoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
           _context.Add(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
         return  await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _dbSet.FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task SaveAsync()
        {
          await  _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
