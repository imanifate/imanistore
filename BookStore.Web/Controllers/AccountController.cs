using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AccountController() : Controller
    {
        [ActionName("CreatAccountAsync")]
        [HttpGet]
        public IActionResult CreatAccountAsync()
        {
            return View();
        }
    }
}
