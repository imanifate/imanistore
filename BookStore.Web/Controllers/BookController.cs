using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Controllers;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController(
        IBookService bookService ,
        ILogger<BookController> logger,
        IBorrowingService borrowingService
        ): BaseController
    {
        [ActionName("GetAllBookAsync")]
        public async Task<IActionResult> GetAllBookAsync(int categoryId)
        {
            ListBookViewModel result = await bookService.GetAllAsync(categoryId);

            return View(result);
        }


        [ActionName("CreateBookAsync")]
        [HttpGet]
       [Authorize(Policy="Adminonly")]
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
                         AlertMessage("  با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
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

        //[ActionName("GetForEditAsync")]
        //[HttpGet]
        //public IActionResult GetForEditAsync(int id)
        //{
        //    if (!ModelState.IsValid) return View();

        //    EditCategoryViewModel model = categoryService.GetForEdit(id);
        //    if (model == null) return View("Error");

        //    return View(model);
        //}

        //[HttpPost("EditCategory")]
        //public IActionResult EditCategory(EditCategoryViewModel model)
        //{
        //    if (!ModelState.IsValid) return View();

        //    EditResult result = categoryService.Edit(model);
        //    switch (result)
        //    {
        //        case EditResult.Success:
        //            {
        //                // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
        //                return RedirectToAction("GetAllCategory");
        //            }
        //        case EditResult.Error:
        //            {
        //                // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
        //                break;
        //            }
        //    }
        //    return View(model);
        //}
        //[ActionName("DeleteBookAsync")]
        //[HttpPost]
        //public IActionResult DeleteCategory(int id)
        //{
        //    DeleteResult result = categoryService.Delete(id);

        //    switch (result)
        //    {
        //        case DeleteResult.Success:
        //            {
        //                // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
        //                return RedirectToAction("GetAllCategory");
        //            }
        //        case DeleteResult.Error:
        //            {
        //                // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
        //                break;
        //            }
        //    }
        //    return View();
        //}

        

            [ActionName("BorrowingAsync")]
        [HttpPost]
        public async Task<IActionResult> BorrowingAsync(int bookId , int categoryId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || bookId == null) return RedirectToAction("GetAllBookAsync", new { categoryId });
            var  userId = int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Result result =await borrowingService.BorrowBookAsync(userId, bookId);
            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage("کتاب امانت داده شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllBookAsync", new {categoryId});
                    }
                case Result.Error:
                    {
                        AlertMessage("فرایند امانت کتاب با مشکل مواجه شد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }

            return View();
        }
    }
}
