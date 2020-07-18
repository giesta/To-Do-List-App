using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services.ToDoList;

namespace WebApplication.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IInMemoryCategoryProvider inMemoryCategoryProvider;

        public CategoryController(IInMemoryCategoryProvider inMemoryCategoryProvider)
        {
            this.inMemoryCategoryProvider = inMemoryCategoryProvider;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            return View(inMemoryCategoryProvider.GetAll());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View(inMemoryCategoryProvider.Get(id));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                category.Id = inMemoryCategoryProvider.GetIndexToInsert();
                inMemoryCategoryProvider.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(inMemoryCategoryProvider.Get(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                inMemoryCategoryProvider.Get(id).Name = category.Name;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(inMemoryCategoryProvider.Get(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {
            try
            {
                inMemoryCategoryProvider.Remove(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
