using Microsoft.AspNetCore.Mvc;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TestLatviaProject.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly UserManager<User> _userManager;
        public TasksController(ITasksRepository tasksRepository, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _tasksRepository = tasksRepository;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(Tasks task)
        {
            await _tasksRepository.Create(task);
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
