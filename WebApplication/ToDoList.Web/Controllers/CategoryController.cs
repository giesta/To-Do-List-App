using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Web.Models;
using ToDoList.Web.Services.ToDoList;

namespace ToDoList.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProviderAsync<Category> categoryProvider;
        public CategoryController(IProviderAsync<Category> categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            return View(await categoryProvider.GetAllAsync());
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryProvider.GetAsync(id);
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
                    await categoryProvider.AddAsync(category);
                    return RedirectToAction(nameof(Index));
             }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryProvider.GetAsync(id);
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
                    await categoryProvider.UpdateAsync(category);
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
            var category = await categoryProvider.GetAsync(id);
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
            try
            {
                await categoryProvider.RemoveAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
            
        }
        private bool CategoryExists(int id)
        {
            if (categoryProvider.GetAsync(id) == null)
                return false;
            return true;
        }
    }
}
