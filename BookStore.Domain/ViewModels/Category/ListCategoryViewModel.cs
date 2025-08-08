using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Category
{
    public class ListCategoryViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public bool IsDelete { get; set; }
    }
}
