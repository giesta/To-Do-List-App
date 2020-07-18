using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class ToDoItemController : Controller
    {
        // GET: ToDoItemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ToDoItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToDoItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
