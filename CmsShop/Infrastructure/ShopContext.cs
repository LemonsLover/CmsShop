using CmsShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            :base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
    }
}
