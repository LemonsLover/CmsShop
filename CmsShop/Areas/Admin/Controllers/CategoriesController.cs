using CmsShop.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderBy(c => c.Sorting).ToListAsync());
        }

        public IActionResult Create() => View();

    }
}
