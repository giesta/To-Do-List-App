using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class ToDoItemsBasicController : Controller
    {
        private readonly WebApplicationContext _context;

        public ToDoItemsBasicController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: ToDoItems1
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDoItem.ToListAsync());
        }

        // GET: ToDoItems1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItems1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status")] ToDoItemDao toDoItemDao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItemDao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItemDao);
        }

        // GET: ToDoItems1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreationDate,DeadLineDate,Priority,Status")] ToDoItemDao toDoItemDao)
        {
            if (id != toDoItemDao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItemDao);
                    await _context.SaveChangesAsync();
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

        // GET: ToDoItems1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}
