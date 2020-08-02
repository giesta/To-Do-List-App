using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services.ToDoList;

namespace WebApplication.Controllers
{
    public class TagsController : Controller
    {
        private readonly IProviderAsync<Tag> tagProvider;
        private readonly IProviderAsync<ToDoItem> toDoItemProvider;
        public TagsController(IProviderAsync<Tag> tagProvider, IProviderAsync<ToDoItem> toDoItemProvider)
        {
            this.tagProvider = tagProvider;
            this.toDoItemProvider = toDoItemProvider;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await tagProvider.GetAllAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tag = await tagProvider.GetAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.ToDoItems = new SelectList(await toDoItemProvider.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {

            if (ModelState.IsValid)
            {
                await tagProvider.AddAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ToDoItems = new SelectList(await toDoItemProvider.GetAllAsync(), "Id", "Name");
            var category = await tagProvider.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await tagProvider.UpdateAsync(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await tagProvider.GetAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Tag tag)
        {
            try
            {
                await tagProvider.RemoveAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tag);
            }

        }

        private bool TagExists(int id)
        {
            if (tagProvider.GetAsync(id) == null)
                return false;
            return true;
        }
    }
}
