using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Book : BaseEntite
    {
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        public bool Borrow { get; set;} = false;
        public DateTime PublicationDate { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        }
}
