using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        
        private readonly IProviderAsync<ToDoItemDao> toDoItemProvider;
        private readonly IProviderAsync<CategoryDao> categoryProvider;
        private readonly IMapper mapper;
        public ToDoItemsController(IProviderAsync<ToDoItemDao> toDoItemProvider, IProviderAsync<CategoryDao> categoryProvider, IMapper mapper)
        {
            this.toDoItemProvider = toDoItemProvider;
            this.categoryProvider = categoryProvider;
            this.mapper = mapper;
        }
        // GET: ToDoItemController
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<IEnumerable<ToDoItemViewModel>>(await toDoItemProvider.GetAllAsync()));
        }

        // GET: ToDoItemController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var toDoItem = await toDoItemProvider.GetAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ToDoItemViewModel>(toDoItem));
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItemDao toDoItem)
        {
            if (ModelState.IsValid)
            {
                await toDoItemProvider.AddAsync(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(mapper.Map<ToDoItemViewModel>(toDoItem));
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
            return View(mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItemDao toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await toDoItemProvider.UpdateAsync(toDoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id))
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
            return View(mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // GET: ToDoItemController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var toDoItem = await toDoItemProvider.GetAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ToDoItemDao toDoItem)
        {
            try
            {
                await toDoItemProvider.RemoveAsync(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(mapper.Map<ToDoItemViewModel>(toDoItem));
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
