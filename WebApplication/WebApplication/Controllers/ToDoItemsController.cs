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
    public class ToDoItemsController : Controller
    {
        private readonly IInMemoryToDoItemProvider inMemoryToDoItemProvider;

        public ToDoItemsController(IInMemoryToDoItemProvider inMemoryToDoItemProvider)
        {
            this.inMemoryToDoItemProvider = inMemoryToDoItemProvider;
        }
        // GET: ToDoItemController
        public ActionResult Index()
        {
            return View(inMemoryToDoItemProvider.GetAll());
        }

        // GET: ToDoItemController/Details/5
        public ActionResult Details(int id)
        {
            return View(inMemoryToDoItemProvider.Get(id));
        }

        // GET: ToDoItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoItem toDoItem)
        {
            try
            {
                toDoItem.Id = inMemoryToDoItemProvider.GetIndexToInsert();
                inMemoryToDoItemProvider.Add(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(inMemoryToDoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoItem toDoItem)
        {
            try
            {
                inMemoryToDoItemProvider.Get(id).Name = toDoItem.Name;
                inMemoryToDoItemProvider.Get(id).Description = toDoItem.Description;
                inMemoryToDoItemProvider.Get(id).Priority = toDoItem.Priority;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(inMemoryToDoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ToDoItem toDoItem)
        {
            try
            {
                inMemoryToDoItemProvider.Remove(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
