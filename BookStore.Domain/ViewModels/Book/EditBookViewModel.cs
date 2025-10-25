using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Book
{
    public class EditBookViewModel
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        public string BookTitle { get; set; }
        public string Author { get; set; }
        public bool IsDeleted { get; set; }
        public bool Borrow { get; set; }
        public string Publisher { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
