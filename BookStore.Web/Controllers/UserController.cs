using System.Threading.Tasks;
using BookStore.Aplication.Services.Implimentation;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class UserController (IUserService userService): Controller
    {

        [ActionName("RgisterUserAsync")]
        [HttpGet]
        public IActionResult RgisterUserAsync()
        {
            return View();
        }
        [ActionName("RgisterUserAsync")]
        [HttpPost]
        public async Task<IActionResult> RgisterUserAsync(RegisterUserViewModl model)
        {
            if(!ModelState.IsValid) return View(model);

          CreatResult result =await userService.RegisterAsync(model);
            if(result == CreatResult.Success)
            {
                return RedirectToAction("CreatAccountAsync", "Account", new { UserId = model.UserId });
            }
            return View();
        }
    }
}
