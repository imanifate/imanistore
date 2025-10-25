using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Account
{
    public class AccountViewModl
    {
        public int UserId { get; set; }

        [Display(Name ="نام کاربری را وارد کنید")]
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "رمز را وارد کنید")]
        [Required(ErrorMessage = "لطفا رمز را  وارد کنید")]
        public string Password { get; set; }

        [Display(Name = " تکرار پسورد")]
        [MaxLength(200)]
        [Compare("Password")]
        [Required(ErrorMessage = "لطفا رمز را مجدد وارد کنید")]
        public string RePassword { get; set; }

        [Display(Name = " ایمیل را وارد کنید")]
        [Required(ErrorMessage = "لطفا ایمیل را  وارد کنید")]
        public string Email { get; set; }
        public string ActiveCode { get; set; } = string.Empty;

        [Display(Name = " قوانین را میپذیرم")]
        [Required(ErrorMessage = "لطفا قوانین  را مطالعه کنید")]
        public bool Rules {  get; set; }
    }
}
