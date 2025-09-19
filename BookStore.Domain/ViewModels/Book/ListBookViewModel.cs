using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Book
{
    public class ListBookViewModel
    {
        public int CategoryId { get; set; }
        public List<GetBookViewModel> Books { get; set; } = new();
    }
}
