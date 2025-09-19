using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Book
{
    public class CreateBookViewModel
    {
        public int CategoryId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;
    }
}
