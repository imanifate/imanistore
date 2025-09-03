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
     
        public string Author { get; set; }
        public bool Borrow { get; set;} = false;
        public DateTime PublicationDate { get; set; }

        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        }
}
