
﻿using System.Threading.Tasks;
using BookStore.Aplication.Services.Implimentation;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Controllers;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{

    public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : BaseController

    {
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            List<GetCategoryViewModel> result = await categoryService.GetAllAsync();
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
                Result result = await categoryService.CreateAsync(model);

                switch (result)
                {
                    case Result.Success:
                        {
                            AlertMessage("ثبت گروه با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                            return RedirectToAction("GetAllCategory");
                        }
                    case Result.Error:
                        {
                            AlertMessage("ثبت گروه با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
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

        [Authorize(Policy = "Adminonly")]
        [ActionName("GetForEdit")]
        [HttpGet]
        public async Task<IActionResult> GetForEdit(int id)
        {
            if (!ModelState.IsValid) return View();

            EditCategoryViewModel model = await categoryService.GetForEdit(id);
            if (model == null)
            {
                AlertMessage("شناسه نامعتبر است", TitleAlert.خطا, IConeAlert.error);

                return View(model);
            }

            return View(model);
        }

        [ActionName("EditCategory")]
        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View();

            Result result = await categoryService.Edit(model);

            {
                logger.LogWarning("CreateCategory POST called with invalid model");
                return View(model);
            }


            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage(" ویرایش با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
                    }
                case Result.Error:
                    {
                        AlertMessage(" ویرایش با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }

            return View(model);
        }

        [ActionName("DeleteCategory")]
        [HttpPost]

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            Result result = await categoryService.Delete(categoryId);

            switch (result)
            {
                case Result.Success:
                    {
                        AlertMessage(" حذف گروه با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
                    }
                case Result.Error:
                    {
                        AlertMessage(" حذف گروه با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return RedirectToAction("GetAllCategory");

        }
    }
}

         
