using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class User : BaseEntite
    {
        public string PhoneNumber { get; set; }
        public string  NationalCode { get; set; }

        public Account Account { get; set; }
        public ICollection<Borrowing> borrowings { get; set; } = new List<Borrowing>();

    }
}
