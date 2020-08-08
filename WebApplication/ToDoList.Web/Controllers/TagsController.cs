using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly IProviderAsync<TagDao> tagProvider;
        private readonly IProviderAsync<ToDoItem> toDoItemProvider;
        public TagsController(IProviderAsync<TagDao> tagProvider, IProviderAsync<ToDoItem> toDoItemProvider, WebApplicationContext context)
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
            ViewBag.ToDoItems = new MultiSelectList(await toDoItemProvider.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagDao tagDao, int[] ToDoItems)
        {
            tagDao.TagToDoItems = new List<TagToDoItem>();
            
            if (ModelState.IsValid)
            {
                foreach (var toDoItemID in ToDoItems)
                {
                    TagToDoItem tagToDoItem = new TagToDoItem { ToDoItemId = toDoItemID, TagId = tagDao.Id };
                    tagDao.TagToDoItems.Add(tagToDoItem);
                }

                await tagProvider.AddAsync(tagDao);
                return RedirectToAction(nameof(Index));
            }
            return View(tagDao);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await tagProvider.GetAsync(id);
            List<int> selectedToDoItems = (await tagProvider.GetAsync(id)).TagToDoItems.Where(m => m.TagId == tag.Id).Select(a => a.ToDoItemId).ToList();
            ViewBag.ToDoItems = new MultiSelectList(await toDoItemProvider.GetAllAsync(), "Id", "Name", selectedToDoItems);
            
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagDao tagDao, int[] ToDoItems)
        {
            if (id != tagDao.Id)
            {
                return NotFound();
            }
            
            tagDao.TagToDoItems = new List<TagToDoItem>();
            await tagProvider.UpdateAsync(tagDao);
            if (ModelState.IsValid)
            {
                foreach (var toDoItemID in ToDoItems)
                {
                    TagToDoItem tagToDoItem = new TagToDoItem { ToDoItemId = toDoItemID, TagId = tagDao.Id };
                    tagDao.TagToDoItems.Add(tagToDoItem);
                }
                try
                {
                    await tagProvider.UpdateAsync(tagDao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tagDao.Id))
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
            return View(tagDao);
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
        public async Task<IActionResult> DeleteConfirmed(TagDao tagDao)
        {
            try
            {
                await tagProvider.RemoveAsync(tagDao);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tagDao);
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
