using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController(IBookService bookService , ILogger<BookController> logger): Controller
    {
        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync(int categoryId)
        {
            ListBookViewModel result = await bookService.GetAllAsync(categoryId);

            return View(result);
        }


        [ActionName("CreateBookAsync")]
        [HttpGet]
        public async Task<IActionResult> CreateBookAsync(int categoryId)
        {
            logger.LogInformation("BookCategory GET called. CategoryId={CategoryId}", categoryId);

            var model = new CreateBookViewModel()
            {
                CategoryId = categoryId
            };
            return View(model);
        }

        [ActionName("CreateBookAsync")]
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync(CreateBookViewModel model)
        {

            if (!ModelState.IsValid)
            {
                logger.LogWarning("CreateBook POST called with invalid model");
                return View(model);
            }
            try
            { 
            Result result = await bookService.CreatAsync(model);

            switch (result)
            {
                case Result.Success:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync");
                    }
                case Result.Error:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating book. Name={Name}", model.BookTitle);
            }
            return View("CreateBookAsync");
        }
    }
}
