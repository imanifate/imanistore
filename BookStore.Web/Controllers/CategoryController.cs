using BookStore.Aplication.Services.Interfaces;
using BookStore.Domain.Enums.Category;
using BookStore.Domain.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController (ICategoryService categoryService) : Controller
    {
        [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            return View();
        }

        [HttpGet("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(CreateCategoryViewModel Createmodel)
        {
          CreatResult result = categoryService.Creat(Createmodel);

            switch (result)
            {
                case CreatResult.Success:
                    {
                       // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
                    }
                case CreatResult.Error:
                    {
                       // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                       break;
                    }
            }
            return View("CreateCategory");
        }

        [HttpGet("GetForEdit")]
        public IActionResult GetForEdit(int id)
        {
            if(!ModelState.IsValid) return View();

           EditCategoryViewModel model = categoryService.GetForEdit(id);
            if (model == null) return View("Error");

            return View(model);
        }

        [HttpPost("EditCategory")]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            if(!ModelState.IsValid) return View();

            EditResult result = categoryService.Edit(model);
            switch (result)
            {
                case EditResult.Success:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
                    }
                case EditResult.Error:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View(model);
        }

        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            DeleteResult result = categoryService.Delete(id);

            switch (result)
            {
                case DeleteResult.Success:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction("GetAllCategory");
                    }
                case DeleteResult.Error:
                    {
                        // AlertMessage("ثبت سوال با موفقیت انجام نشد", TitleAlert.خطا, IConeAlert.error);
                        break;
                    }
            }
            return View();
        }
    }
}
