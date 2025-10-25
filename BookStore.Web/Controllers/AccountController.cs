using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Aplication.Services.Interfaces;
using BookStore.Aplication.Utilities;
using BookStore.Controllers;
using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Domain.ViewModels;
using BookStore.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace BookStore.Web.Controllers
{
    public class AccountController(IAccountService accountService) : BaseController
    {
        [ActionName("CreatAccountAsync")]
        [HttpGet]
        public IActionResult CreatAccountAsync(int userId)
        {

            return View(new AccountViewModl
            {
                UserId = userId,
            });
        }
        [ActionName("CreatAccountAsync")]
        [HttpPost]
        public async Task<IActionResult> CreatAccountAsync(AccountViewModl model)
        {
            if (!ModelState.IsValid) return View();

            model.ActiveCode = CodeGenerators.ActiveCode();

            Result result = await accountService.CreatAccountAsync(model);

            switch (result)
            {
                case Result.Success:
                    {
                        MessageSender.Email(model.Email, "", "به کتاب خانه ما خوش امدید" +
                            $"{Environment.NewLine} کد فعال سازی:{model.ActiveCode}");
                        AlertMessage("کد فعال سازی به ایمیل شما ارسال شد", TitleAlert.موفق, IConeAlert.success);
                        return RedirectToAction(nameof(Active));
                    }
                case Result.EmailDuplicated:
                    {
                        AlertMessage("ایمیل وارد شده تکراری است", TitleAlert.خطا, IConeAlert.error);
                    }
                    break;
                case Result.UsernaemDuplicated:
                    {
                        AlertMessage("نام کاربری وارد شده تکراری است", TitleAlert.خطا, IConeAlert.error);
                    }
                    break;
                case Result.Error:
                    {
                        AlertMessage("ثبت نام با خطا مواجه شد", TitleAlert.هشدار, IConeAlert.warning);
                    }
                    break;
                default:
                    AlertMessage("ثبت نام با خطا مواجه شد", TitleAlert.هشدار, IConeAlert.error);
                    break;
            }
            return View(model);
        }

        [ActionName("Active")]
        [HttpGet]
        public IActionResult Active()
        {
            return View();
        }

        [ActionName("Active")]
        [HttpPost]
        public async Task<IActionResult> Active(ActiveViewModel model)
        {
            if (!ModelState.IsValid) return View(nameof(Active));

         Result result =await  accountService.AccountActiveAsync(model.ActiveCode);
            switch (result)
            {
                case Result.Success: return RedirectToAction(nameof(LoginAsync));
                case Result.Error:
                    AlertMessage("کد فعال سازی اشتباه است", TitleAlert.هشدار, IConeAlert.error); break;
            }

            return View();
        }

        [ActionName("LoginAsync")]
        [HttpGet]
        public IActionResult LoginAsync()
        {
            return View();
        }

        [ActionName("LoginAsync")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Account account =await accountService.LoginAsync(model);
            //  لاگین کردن ینی چک کنی نام و پسورد درسته یا نه
            if(account == null)
            {
                AlertMessage("نام کاربری یا رمز عبور اشتباه است", TitleAlert.خطا, IConeAlert.error);
                return View(model);
            }
            // بعد از لاگین کردن احراز هویت میکنیم و همچنین اطلاعات اولیه کاربر مثل نام یا ایدی را در توکن یا کوکی ذخیره میکنیم
            List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Email, account.Email),
            new Claim(ClaimTypes.Name, account.Title),
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim("IsAdmin", account.IsAdmin.ToString())
        };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
            AuthenticationProperties properties = new() { IsPersistent = model.RemmberMe };
            HttpContext.SignInAsync(claimsPrincipal, properties);
      
            return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
