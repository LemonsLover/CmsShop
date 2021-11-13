using CmsShop.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(c => c.Name).Include(x => x.Category).ToListAsync());
        }

        public IActionResult Create() 
        {
            ViewBag.CategotyId = new SelectList(_context.Categories.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Page page)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        page.Slug = page.Title.ToLower().Replace(" ", "-");
        //        page.Sorting = 100;

        //        var slug = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == page.Slug);
        //        if (slug != null)
        //        {
        //            ModelState.AddModelError("", "The page alaready exists.");
        //            return View(page);
        //        }
        //        _context.Pages.Add(page);
        //        _context.SaveChanges();

        //        TempData["Success"] = "The page has been added!";

        //        return RedirectToAction("Index");
        //    }
        //    return View(page);
        //}
    }
}
