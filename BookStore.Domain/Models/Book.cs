using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Book : BaseEntite
    {
     
        public string Author { get; set; }
       
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public ICollection<Borrowing> borrowings { get; set; } =new List<Borrowing>();
        }
}
