using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.ViewModels.Category;

namespace BookStore.Web.Component
{
    public class CategoryItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GetCategoryViewModel categoryList)
        {
            return View("CategoryItem", categoryList);
        }
    }
}
