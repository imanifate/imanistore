using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookStore.Domain.Enums;
using BookStore.Domain.ViewModels;
namespace BookStore.Controllers;

public class BaseController : Controller
{
    protected void AlertMessage(string message, TitleAlert titleAlert, IConeAlert iconeAlert)
    {
        TempData["Alert"] = JsonConvert.SerializeObject(new AlertMessageViewModel
        {
            Title = titleAlert.ToString(),
            Text = message,
            Icon = iconeAlert.ToString(),
        });
    }
}
