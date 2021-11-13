using CmsShop.Infrastructure;
using CmsShop.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category alaready exists.");
                    return View(category);
                }
                _context.Categories.Add(category);
                _context.SaveChanges();

                TempData["Success"] = "The category has been added!";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Categories.Where(c => c.Id != id).FirstOrDefaultAsync(c => c.Slug == category.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The category alaready exists.");
                    return View(category);
                }
                _context.Categories.Update(category);
                _context.SaveChanges();

                TempData["Success"] = "The category has been edited!";

                return RedirectToAction("Edit", new { id });
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The category has been removed!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "The category does not exist!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 0;
            foreach (var categoryId in id)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                category.Sorting = count;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
