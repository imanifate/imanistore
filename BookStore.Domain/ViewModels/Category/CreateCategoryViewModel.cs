using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        public int? CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string? Parent { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryTitle { get; set; }

    }
}
