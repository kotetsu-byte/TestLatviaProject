using Microsoft.AspNetCore.Mvc;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TestLatviaProject.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace TestLatviaProject.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public TasksController(ITasksRepository tasksRepository, UserManager<User> userManager, IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = _mapper.Map<List<TasksDto>>(await _tasksRepository.GetAllTasks());
            return View(tasks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = _mapper.Map<TasksDto>(await _tasksRepository.GetTaskById(id));

            return View(task);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TasksDto taskDto)
        {
            var task = _mapper.Map<Tasks>(taskDto);
            await _tasksRepository.Create(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var task = _mapper.Map<TasksDto>(await _tasksRepository.GetTaskById(id));
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(
                new SelectListItem()
                {
                    Text = "Incompleted",
                    Value = "Incompleted"
                }
            );
            list.Add(
                new SelectListItem()
                {
                    Text = "In Progress",
                    Value = "In Progress"
                }
            );
            list.Add(
                new SelectListItem()
                {
                    Text = "Completed",
                    Value = "Completed"
                }
            );
            task.StatusList = list;
            return View(task);
        }

        [HttpPost]
        public IActionResult Update(TasksDto taskDto)
        {
            var task = _mapper.Map<Tasks>(taskDto);
            _tasksRepository.Update(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = _mapper.Map<TasksDto>(await _tasksRepository.GetTaskById(id));

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
