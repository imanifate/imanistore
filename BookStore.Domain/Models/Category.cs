using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Models;

namespace BookStore.Domain.Models
{
    public class Category : BaseEntite
    {
        public int? ParentId { get; set; }
        public  Category? Parent { get; set; }
        public  ICollection<Category> Children { get; set; } = new List<Category>();
    }
}
