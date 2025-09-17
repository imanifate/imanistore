using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController: Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync()
        {
            List<GetBookViewModel> result = await _bookService.GetAllAsync();
            return View(result);
        }


        [HttpGet("CreateBookAsync")]
        public async Task<IActionResult> CreateBookAsync(int categoryId)
        {
            var model = new CreateBookViewModel()
            {
                CategoryId = categoryId
            };
            return View(model);
        }

        [HttpPost("CreateBookAsync")]
        public async Task<IActionResult> CreateBookAsync(CreateBookViewModel model)
        {
            CreatResult result = await _bookService.CreatAsync(model);
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
            return View("CreateCategory");
        }
    }
}
