using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly IProviderAsync<TagDao> tagProvider;
        private readonly IProviderAsync<ToDoItemDao> toDoItemProvider;
        private readonly IMapper mapper;
        public TagsController(IProviderAsync<TagDao> tagProvider, IProviderAsync<ToDoItemDao> toDoItemProvider, WebApplicationContext context, IMapper mapper)
        {
            this.tagProvider = tagProvider;
            this.toDoItemProvider = toDoItemProvider;
            this.mapper = mapper;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map < IEnumerable < TagViewModel >> (await tagProvider.GetAllAsync()));
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tag = await tagProvider.GetAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(mapper.Map<TagViewModel>(tag));
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
        public async Task<IActionResult> Create(TagDao tag, int[] ToDoItems)
        {
            tag.TagToDoItems = new List<TagToDoItemDao>();
            
            if (ModelState.IsValid)
            {
                foreach (var toDoItemID in ToDoItems)
                {
                    TagToDoItemDao tagToDoItemDao = new TagToDoItemDao { ToDoItemId = toDoItemID, TagId = tag.Id };
                    tag.TagToDoItems.Add(tagToDoItemDao);
                }

                await tagProvider.AddAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(mapper.Map<TagViewModel>(tag));
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
            return View(mapper.Map<TagViewModel>(tag));
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagDao tag, int[] ToDoItems)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }
            
            tag.TagToDoItems = new List<TagToDoItemDao>();
            await tagProvider.UpdateAsync(tag);
            if (ModelState.IsValid)
            {
                foreach (var toDoItemID in ToDoItems)
                {
                    TagToDoItemDao tagToDoItemDao = new TagToDoItemDao { ToDoItemId = toDoItemID, TagId = tag.Id };
                    tag.TagToDoItems.Add(tagToDoItemDao);
                }
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
            return View(mapper.Map<TagViewModel>(tag));
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await tagProvider.GetAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TagViewModel>(tag));
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TagDao tag)
        {
            try
            {
                await tagProvider.RemoveAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(mapper.Map<TagViewModel>(tag));
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
