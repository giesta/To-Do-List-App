using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Services.ToDoList;

namespace WebApplication.Controllers
{
    public class CategoryController : Controller
    {

        //private readonly ICategoryProvider inMemoryCategoryProvider;
        //private readonly IGenericProvider<Category> categoryProvider;
        private readonly IGenericProvider<Category> categoryProvider;
        //public CategoryController(ICategoryProvider inMemoryCategoryProvider)
        //{
        //    this.inMemoryCategoryProvider = inMemoryCategoryProvider;
        //}
        public CategoryController(IGenericProvider<Category> categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(()=>categoryProvider.GetAll()));
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await Task.Run(() => categoryProvider.Get(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
             if (ModelState.IsValid)
              {
                    await Task.Run(()=>categoryProvider.Add(category));
                    return RedirectToAction(nameof(Index));
             }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await Task.Run(()=> categoryProvider.Get(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(()=>categoryProvider.Update(category));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await Task.Run(()=>categoryProvider.Get(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Category category)
        {            
            await Task.Run(()=>categoryProvider.Remove(category));
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            if (categoryProvider.Get(id) == null)
                return false;
            return true;
        }
    }
}
