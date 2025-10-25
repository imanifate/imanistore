using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Category
{
    public class GetCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }

      
        public bool IsDeleted { get; set; }

       public int ChildrenCount {get; set; }
        public List<GetCategoryViewModel> Childrens { get; set; } = new List<GetCategoryViewModel>();


    }
}
