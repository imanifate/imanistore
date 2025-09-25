using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.User
{
    public class RegisterUserViewModl
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
       public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
    }
}
