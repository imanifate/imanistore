using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Account: BaseEntite
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string PasswordHash { get; set; } 
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string ActiveCode { get; set; }
       
    }
}
