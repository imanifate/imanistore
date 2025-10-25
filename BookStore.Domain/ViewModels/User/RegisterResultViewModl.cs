using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Enums;

namespace BookStore.Domain.ViewModels.User
{
    public class RegisterResultViewModl
    {
        public int UserId { get; set; }
        public Result Status { get; set; }
      
    }
}
