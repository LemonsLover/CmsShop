using CmsShop.Infrastructure;
using CmsShop.Models;
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

        public async Task<IActionResult> Details(int id)
        {
            Page page = await _context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            if (page == null)
                return NotFound();

            return View(page);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page) 
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == page.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "The page alaready exists.");
                    return View(page);
                }    
                _context.Pages.Add(page);
                _context.SaveChanges();

                TempData["Success"] = "The page has been added!";

                return RedirectToAction("Index");
            }
            return View(page);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Page page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.Where(p => p.Id != page.Id).FirstOrDefaultAsync(p => p.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The page alaready exists.");
                    return View(page);
                }
                _context.Pages.Update(page);
                _context.SaveChanges();

                TempData["Success"] = "The page has been edited!";

                return RedirectToAction("Edit", new { id = page.Id});
            }
            return View(page);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null && page.Title != "Home")
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The page has been removed!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "The page does not exist!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;
            foreach(var pageId in id)
            {
                Page page = await _context.Pages.FindAsync(pageId);
                page.Sorting = count;
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }

    }
}
