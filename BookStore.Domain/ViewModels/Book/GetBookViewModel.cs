using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Book
{
    public class GetBookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsDeleted { get; set; }
        public bool Borrow { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
