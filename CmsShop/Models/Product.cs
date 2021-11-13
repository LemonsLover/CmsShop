using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimun leght is 2")]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategotyId { get; set; }

        public string Image { get; set; }

        [ForeignKey("CategotyId")]
        public virtual Category Category { get; set; }
    }
}
