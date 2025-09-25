using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Account
{
    public class AccountViewModl
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; } 
        public string RePassword { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string ActiveCode { get; set; }
        public bool Rules {  get; set; }
    }
}
