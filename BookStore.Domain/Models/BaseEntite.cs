using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class BaseEntite
    {
        [Key]
        public int Id { get; set; }

        public bool IsDelete { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

    }
}
