using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Services.ToDoList;

namespace WebApplication.Controllers
{
    public class ToDoItemsController : Controller
    {
        
        private readonly IGenericProviderAsync<ToDoItem> toDoItemProvider;
        private IGenericProviderAsync<Category> categoryProvider;
        public ToDoItemsController(IGenericProviderAsync<ToDoItem> toDoItemProvider, IGenericProviderAsync<Category> categoryProvider)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                await toDoItemProvider.AddAsync(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status, CategoryID")] ToDoItem toDoItem)
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
            return View(toDoItem);
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
        public async Task<IActionResult> DeleteConfirmed(ToDoItem toDoItem)
        {
            await toDoItemProvider.RemoveAsync(toDoItem);
            return RedirectToAction(nameof(Index));
        }
        private bool ToDoItemExists(int id)
        {
            if (toDoItemProvider.GetAsync(id) == null)
                return false;
            return true;
        }
    }
}
