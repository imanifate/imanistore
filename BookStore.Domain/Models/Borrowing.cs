using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Borrowing : BaseEntite
    {
        public int BookId { get; set; }
       public Book Book {  get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

       public DateTime? ReturnDate { get; set; }

        public bool IsReturn {  get; set; }
    }
}
