using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Repositories;
using System;

namespace ToDoApp.Controllers

{
    public class ToDoController: Controller
    {
        private readonly ToDoRepository _repository;

        public ToDoController()
        {
            _repository = new ToDoRepository();
        }

        public async Task<IActionResult> Index()
        {
            var items = await _repository.GetAllAsync();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDoItem toDoItems)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(toDoItems);
                return RedirectToAction(nameof(Index));
            }
              // Log validation errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation error: " + error.ErrorMessage);
            }
            return View(toDoItems);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(id, toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }    
}