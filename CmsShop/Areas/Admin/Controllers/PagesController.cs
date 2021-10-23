using CmsShop.Infrastructure;
using CmsShop.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ShopContext _context;

        public PagesController(ShopContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in _context.Pages orderby p.Sorting select p;

            List<Page> pageList = await pages.ToListAsync();

            return View(pageList);
        }

        public string Edit(int id)
        {
            return id.ToString();
        }

        public async Task<IActionResult> Details(int id)
        {
            Page page = await _context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            if (page == null)
                return NotFound();

            return View(page);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Page page) 
        {
            page.Slug = page.Title.ToLower().Replace(" ", "-");
            page.Sorting = 100;
            _context.Pages.Add(page);
            _context.SaveChanges();
            return new OkResult();
        }
    }
}
