using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.Controllers
{
    public class TagToDoItemBasicController : Controller
    {
        private readonly WebApplicationContext _context;

        public TagToDoItemBasicController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: TagToDoItemDaos
        public async Task<IActionResult> Index()
        {
            var webApplicationContext = _context.TagToDoItem.Include(t => t.TagDao).Include(t => t.ToDoItemDao);
            return View(await webApplicationContext.ToListAsync());
        }

        // GET: TagToDoItemDaos/Details/5
        public async Task<IActionResult> Details(int? tagId, int? toDoItemId)
        {
            if (tagId == null || toDoItemId==null)
            {
                return NotFound();
            }

            var tagToDoItemDao = await _context.TagToDoItem
                .Include(t => t.TagDao)
                .Include(t => t.ToDoItemDao)
                .FirstOrDefaultAsync(m => m.TagId == tagId && m.ToDoItemId == toDoItemId);
            if (tagToDoItemDao == null)
            {
                return NotFound();
            }

            return View(tagToDoItemDao);
        }

        // GET: TagToDoItemDaos/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name");
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name");
            return View();
        }

        // POST: TagToDoItemDaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,ToDoItemId")] TagToDoItemDao tagToDoItemDao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagToDoItemDao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", tagToDoItemDao.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", tagToDoItemDao.ToDoItemId);
            return View(tagToDoItemDao);
        }

        // GET: TagToDoItemDaos/Edit/5
        public async Task<IActionResult> Edit(int? tagId, int? toDoItemId)
        {
            if (tagId == null || toDoItemId == null)
            {
                return NotFound();
            }

            var tagToDoItemDao = await _context.TagToDoItem.FindAsync(tagId, toDoItemId);
            if (tagToDoItemDao == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name", tagToDoItemDao.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", tagToDoItemDao.ToDoItemId);
            ViewData["OldTagId"] = tagId;
            ViewData["OldToDoItemId"] = toDoItemId;
            return View(tagToDoItemDao);
        }

        // POST: TagToDoItemDaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldTagId, int? oldToDoItemId, [Bind("TagId,ToDoItemId")] TagToDoItemDao tagToDoItemDao)
        {
            
            if (tagToDoItemDao == null)
            {
                Console.WriteLine("yra");
                return NotFound();
            }

            var oldTagToDoItem = await _context.TagToDoItem.FindAsync(oldTagId, oldToDoItemId);

            if (oldTagToDoItem == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Remove(oldTagToDoItem);
                    _context.Add(tagToDoItemDao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagToDoItemDaoExists(tagToDoItemDao.TagId))
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
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name", tagToDoItemDao.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", tagToDoItemDao.ToDoItemId);
            return View(tagToDoItemDao);
        }

        // GET: TagToDoItemDaos/Delete/5
        public async Task<IActionResult> Delete(int? tagId, int? toDoItemId)
        {
            if (tagId == null || toDoItemId == null)
            {
                return NotFound();
            }

            var tagToDoItemDao = await _context.TagToDoItem
                .Include(t => t.TagDao)
                .Include(t => t.ToDoItemDao)
                .FirstOrDefaultAsync(m => m.TagId == tagId && m.ToDoItemId == toDoItemId);
            if (tagToDoItemDao == null)
            {
                return NotFound();
            }

            return View(tagToDoItemDao);
        }

        // POST: TagToDoItemDaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? tagId, int? toDoItemId)
        {
            var tagToDoItemDao = await _context.TagToDoItem.FindAsync(tagId, toDoItemId);
            _context.TagToDoItem.Remove(tagToDoItemDao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagToDoItemDaoExists(int id)
        {
            return _context.TagToDoItem.Any(e => e.TagId == id);
        }
    }
}
