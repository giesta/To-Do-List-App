﻿using System;
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
        //private readonly IToDoItemProvider inMemoryToDoItemProvider;
        private readonly IGenericProvider<ToDoItem> toDoItemProvider;
        //public ToDoItemsController(IToDoItemProvider inMemoryToDoItemProvider)
        //{
        //    this.inMemoryToDoItemProvider = inMemoryToDoItemProvider;
        //}
        public ToDoItemsController(IGenericProvider<ToDoItem> toDoItemProvider)
        {
            this.toDoItemProvider = toDoItemProvider;
        }
        // GET: ToDoItemController
        public ActionResult Index()
        {
            return View(toDoItemProvider.GetAll());
        }

        // GET: ToDoItemController/Details/5
        public ActionResult Details(int id)
        {
            return View(toDoItemProvider.Get(id));
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
                toDoItem.Id = toDoItemProvider.GetIndexToInsert();
                toDoItemProvider.Add(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
            }
        }

        // GET: ToDoItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(toDoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ToDoItem toDoItem)
        {
            try
            {                
                toDoItemProvider.Update(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
            }
        }

        // GET: ToDoItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(toDoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ToDoItem toDoItem)
        {
            try
            {
                toDoItemProvider.Remove(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
            }
        }
    }
}
