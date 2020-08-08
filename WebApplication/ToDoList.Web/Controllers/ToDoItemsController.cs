using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        
        private readonly IProviderAsync<ToDoItemDao> toDoItemProvider;
        private readonly IProviderAsync<CategoryDao> categoryProvider;
        public ToDoItemsController(IProviderAsync<ToDoItemDao> toDoItemProvider, IProviderAsync<CategoryDao> categoryProvider)
        {
            this.toDoItemProvider = toDoItemProvider;
            this.categoryProvider = categoryProvider;
        }
        // GET: ToDoItemController
        public async Task<IActionResult> Index()
        {
            return View(await toDoItemProvider.GetAllAsync());
        }

        // GET: ToDoItemController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var toDoItem = await toDoItemProvider.GetAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }


        // GET: ToDoItemController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Category = new SelectList(await categoryProvider.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: ToDoItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItemDao toDoItemDao)
        {
            if (ModelState.IsValid)
            {
                await toDoItemProvider.AddAsync(toDoItemDao);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItemDao);
        }

        // GET: ToDoItemController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Category = new SelectList(await categoryProvider.GetAllAsync(), "Id", "Name");
            var toDoItem = await toDoItemProvider.GetAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItemDao toDoItemDao)
        {
            if (id != toDoItemDao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await toDoItemProvider.UpdateAsync(toDoItemDao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItemDao.Id))
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
            return View(toDoItemDao);
        }

        // GET: ToDoItemController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var toDoItem = await toDoItemProvider.GetAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ToDoItemDao toDoItemDao)
        {
            try
            {
                await toDoItemProvider.RemoveAsync(toDoItemDao);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItemDao);
            }
            
        }
        private bool ToDoItemExists(int id)
        {
            if (toDoItemProvider.GetAsync(id) == null)
                return false;
            return true;
        }
    }
}
