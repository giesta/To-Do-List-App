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

        //private readonly ICategoryProvider inMemoryCategoryProvider;
        private readonly IGenericProvider<Category> categoryProvider;

        //public CategoryController(ICategoryProvider inMemoryCategoryProvider)
        //{
        //    this.inMemoryCategoryProvider = inMemoryCategoryProvider;
        //}
        public CategoryController(IGenericProvider<Category> categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            return View(categoryProvider.GetAll());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View(categoryProvider.Get(id));
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
                category.Id = categoryProvider.GetIndexToInsert();
                categoryProvider.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(categoryProvider.Get(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                categoryProvider.Update(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(categoryProvider.Get(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {
            try
            {
                categoryProvider.Remove(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
    }
}
