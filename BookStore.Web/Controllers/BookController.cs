using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController: Controller
    {
<<<<<<< HEAD
        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync(int categoryId)
        {
            ListBookViewModel result = await bookService.GetAllAsync(categoryId);
=======
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync()
        {
            List<GetBookViewModel> result = await _bookService.GetAllAsync();
>>>>>>> 7de73b1b9159c7d47ead5d6ffe0cfc72b459b5ad
            return View(result);
        }


        [ActionName("CreateBookAsync")]
        [HttpGet]
        public async Task<IActionResult> CreateBookAsync(int categoryId)
        {
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
<<<<<<< HEAD
            if(!ModelState.IsValid) return View(model);
            CreatResult result = await bookService.CreatAsync(model);
=======
            CreatResult result = await _bookService.CreatAsync(model);
>>>>>>> 7de73b1b9159c7d47ead5d6ffe0cfc72b459b5ad
            switch (result)
            {
                case CreatResult.Success:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync");
                    }
                case CreatResult.Error:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View("CreateBookAsync");
        }
    }
}
