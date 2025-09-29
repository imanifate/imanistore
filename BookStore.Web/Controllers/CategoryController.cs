using BookStore.Aplication.Services.Implimentation;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController (ICategoryService categoryService , ILogger<CategoryController> logger) : Controller
    {
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
           List<GetCategoryViewModel> result =await categoryService.GetAllAsync();
            return View(result);
        }

        [Authorize(Policy = "Adminonly")]
        [HttpGet("CreateCategory")]
        public IActionResult CreateCategory(int? categoryId)
        {
            logger.LogInformation("CreateCategory GET called. CategoryId={CategoryId}", categoryId);

            var model = new CreateCategoryViewModel
            {
                CategoryId = categoryId
            };

            return View(model);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("CreateCategory POST called with invalid model");
                return View(model);
            }

            try
            {
                Result result =await categoryService.CreateAsync(model);

            switch (result)
            {
                case Result.Success:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
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
                logger.LogError(ex, "Unexpected error while creating category. Name={Name}", model.CategoryTitle);
            }

            return View("CreateCategory", model);
        }
         

        //    [HttpGet("GetForEdit")]
        //    public IActionResult GetForEdit(int id)
        //    {
        //        if(!ModelState.IsValid) return View();

        //       EditCategoryViewModel model = categoryService.GetForEdit(id);
        //        if (model == null) return View("Error");

        //        return View(model);
        //    }

        //    [HttpPost("EditCategory")]
        //    public IActionResult EditCategory(EditCategoryViewModel model)
        //    {
        //        if(!ModelState.IsValid) return View();

        //        EditResult result = categoryService.Edit(model);
        //        switch (result)
        //        {
        //            case EditResult.Success:
        //                {
        //                    // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
        //                    return RedirectToAction("GetAllCategory");
        //                }
        //            case EditResult.Error:
        //                {
        //                    // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
        //                    break;
        //                }
        //        }
        //        return View(model);
        //    }

        //    [HttpPost("DeleteCategory")]
        //    public IActionResult DeleteCategory(int id)
        //    {
        //        DeleteResult result = categoryService.Delete(id);

        //        switch (result)
        //        {
        //            case DeleteResult.Success:
        //                {
        //                    // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
        //                    return RedirectToAction("GetAllCategory");
        //                }
        //            case DeleteResult.Error:
        //                {
        //                    // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
        //                    break;
        //                }
        //        }
        //        return View();
        //    }
    }
}
