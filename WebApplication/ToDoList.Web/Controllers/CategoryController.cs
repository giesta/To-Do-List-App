using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProviderAsync<CategoryDao> categoryProvider;
        public CategoryController(IProviderAsync<CategoryDao> categoryProvider)
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
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryDao categoryDao)
        {
             if (ModelState.IsValid)
              {
                    await categoryProvider.AddAsync(categoryDao);
                    return RedirectToAction(nameof(Index));
             }
            return View(categoryDao);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryDao categoryDao)
        {
            if (id != categoryDao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryProvider.UpdateAsync(categoryDao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryDao.Id))
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
            return View(categoryDao);
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
        public async Task<IActionResult> DeleteConfirmed(CategoryDao categoryDao)
        {
            try
            {
                await categoryProvider.RemoveAsync(categoryDao);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoryDao);
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
