using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Controllers;
using BookStore.Data.Repositores;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController(
        IBookService bookService,
        ILogger<BookController> logger,
        IBorrowingService borrowingService
        ) : BaseController
    {
        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync(int categoryId)
        {
            ListBookViewModel result = await bookService.GetAllAsync(categoryId);
            ViewData["Title"] = "لیست کتابها";
            return View(result);
        }


        [ActionName("CreateBookAsync")]
        [HttpGet]
        [Authorize(Policy = "Adminonly")]
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
                            AlertMessage("  ثبت کتاب با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                            return RedirectToAction("GetAllBookAsync", new {CategoryId = model.CategoryId });
                        }
                    case Result.Error:
                        {
                             AlertMessage("ثبت کتاب با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
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

        [ActionName("EditBookAsync")]
        [HttpGet]
        public async Task<IActionResult> EditBookAsync(int bookId)
        {
            if (!ModelState.IsValid) return View();

            EditBookViewModel model =await bookService.GetForEditAsync(bookId);
            if (model == null)
            {
                AlertMessage(" شناسه نامعتبر است.", TitleAlert.خطا, IConeAlert.error);

                return View(model); }

            return View(model);
        }

        [ActionName("EditBookAsync")]
        [HttpPost]
        public async Task<IActionResult> EditBookAsync(EditBookViewModel model)
        {
            if (!ModelState.IsValid) return View();

            Result result =await bookService.EditAsync(model);
            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage(" ویرایش کتاب با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new {model.CategoryId});
                    }
                case Result.Error:
                    {
                        AlertMessage(" ویرایش کتاب با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View(model);
        }

        [ActionName("DeleteBookAsync")]
        [HttpPost]
        public async Task<IActionResult> DeleteBookAsync(int bookId , int categoryId)
        {
            Result result =await bookService.DeleteAsync(bookId);

            switch (result)
            {
                case Result.Success:
                    {
                         AlertMessage(" حذف با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new {categoryId});
                    }
                case Result.Error:
                    {
                        AlertMessage(" حذف با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View();
        }

        [ActionName("UnDeleteBookAsync")]
        [HttpPost]
        public async Task<IActionResult> UnDeleteBookAsync(int bookId, int categoryId)
        {
            Result result = await bookService.UnDeleteAsync(bookId);

            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage(" لغو حذف با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new { categoryId });
                    }
                case Result.Error:
                    {
                        AlertMessage(" لغو حذف با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View();
        }


        [ActionName("BorrowingAsync")]
        [HttpPost]
        public async Task<IActionResult> BorrowingAsync(int bookId, int categoryId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || bookId == null) return RedirectToAction("GetAllBookAsync", new { categoryId });
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Result result = await borrowingService.BorrowBookAsync(userId, bookId);
            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage("کتاب امانت داده شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new { categoryId });
                    }
                case Result.Error:
                    {
                        AlertMessage("فرایند امانت کتاب با مشکل مواجه شد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
                default:
                    {
                        AlertMessage("فرایند امانت کتاب با مشکل مواجه شد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }

            return View();
        }
        [ActionName("ReturnBookAsync")]
        [HttpPost]
        public async Task<IActionResult> ReturnBookAsync(int bookId,int categoryId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || bookId == null) return RedirectToAction("GetAllBookAsync", new { categoryId });
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Result result = await borrowingService.ReturnBookAsync(userId, bookId);
            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage("کتاب پس داده شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new { categoryId });
                    }
                case Result.Error:
                    {
                        AlertMessage("فرایند پس دادن کتاب با مشکل مواجه شد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
                default:
                    {
                        AlertMessage("فرایند پس دادن کتاب با مشکل مواجه شد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View();
        }

        [ActionName("SearchBookAndAuthorasync")]
        [HttpGet]
        public async Task<IActionResult> SearchBookAndAuthorasync()
        {
            return View();
        }

        [ActionName("SearchBookAndAuthorasync")]
        [HttpPost]
        public async Task<IActionResult> SearchBookAndAuthorasync(string bookAndAuthor)
        {
            if (bookAndAuthor == null)
            {
                AlertMessage("فیلد جستجو خالی میباشد", TitleAlert.خطا, IConeAlert.error);
            }

            var result =await bookService.SearchTitleAsync(bookAndAuthor);
            if (result == null)

            {
                AlertMessage("نتیجه ای یافت نشد", TitleAlert.خطا, IConeAlert.error);
                return View();
            }          
            return View( result);
            
        }


        [ActionName("SearchAuthorasync")]
        [HttpGet]
        public async Task<IActionResult> SearchAuthorasync()
        {
            
            return View();
        }

        [ActionName("SearchAuthorasync")]
        [HttpPost]
        public async Task<IActionResult> SearchAuthorasync(string Author)
        {
            ViewData["Title"] = "جستجو";
            if (Author == null)
            {
                AlertMessage("فیلد جستجو خالی میباشد", TitleAlert.خطا, IConeAlert.error);
            }

            var result = await bookService.SearchAuthorAsync(Author);
            if (result == null)

            {
                AlertMessage("نتیجه ای یافت نشد", TitleAlert.خطا, IConeAlert.error);
                return View();
            }


            return View(result);

        }
    }
}
