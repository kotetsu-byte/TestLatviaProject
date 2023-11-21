using Microsoft.AspNetCore.Mvc;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksRepository _tasksRepository;
        

        public TasksController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _tasksRepository.GetAllTasks();
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _tasksRepository.GetTaskById(id);

            return View(task);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tasks task)
        {
            _tasksRepository.Create(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var task = await _tasksRepository.GetTaskById(id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Update(Tasks task)
        {
            _tasksRepository.Update(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _tasksRepository.GetTaskById(id);

            return View(task);
        }

        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            _tasksRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
