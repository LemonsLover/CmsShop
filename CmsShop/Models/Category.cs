using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimun leght is 2")]
        [RegularExpression(@"^[a-zA-z]+$", ErrorMessage = "Only english letters are allowed")]
        public string Name { get; set;}
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}
