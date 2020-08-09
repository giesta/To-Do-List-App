using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Business.Profiles;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProviderAsync<Category> categoryProvider;
        private readonly IMapper mapper;
        public CategoryController(IProviderAsync<Category> categoryProvider, IMapper mapper)
        {
            this.categoryProvider = categoryProvider;
            this.mapper = mapper;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map< IEnumerable < Category > ,IEnumerable <CategoryViewModel>>(await categoryProvider.GetAllAsync()));
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryProvider.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(mapper.Map <  CategoryViewModel > (category));
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
                    await categoryProvider.AddAsync(mapper.Map<Category>(category));
                    return RedirectToAction(nameof(Index));
             }
            return View(mapper.Map<CategoryViewModel>(category));
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryProvider.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(mapper.Map<CategoryViewModel>(category));
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
            return View(mapper.Map<CategoryViewModel>(category));
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryProvider.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(mapper.Map<CategoryViewModel>(category));
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
                return View(mapper.Map<CategoryViewModel>(category));
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
