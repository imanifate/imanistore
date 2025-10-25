using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری یا ایمیل")]
        [MaxLength(200)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserNameOrEmail { get; set; }

        [Display(Name = " پسورد")]
        [MaxLength(200)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        public bool RemmberMe { get; set; }


    }


}
