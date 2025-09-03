using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Contracts
{
    public interface IGenericRepository <T> where T : BaseEntite
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        void Add(T entity);
        void Update (T entity);
        Task SaveAsync();
    }
}
