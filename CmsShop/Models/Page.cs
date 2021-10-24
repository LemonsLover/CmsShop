using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimun length is 2 !")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(5, ErrorMessage = "Minimun length is 5 !")]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
