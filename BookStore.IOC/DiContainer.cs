using Microsoft.Extensions.DependencyInjection;
using BookStore.Aplication.Services.Implimentation;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Data.Repositores;
using BookStore.Domain.Contracts;

namespace Store.IOC
{
    public static class DiContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositores
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            #endregion

            #region Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            #endregion
        }
    }
}
