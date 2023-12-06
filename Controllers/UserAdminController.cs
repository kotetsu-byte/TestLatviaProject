using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestLatviaProject.Dtos;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Controllers
{
    [Authorize]
    public class UserAdminController : Controller
    {
        private readonly IUserAdminRepository _userAdminRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserAdminController(IUserAdminRepository userAdminRepository, UserManager<User> userManager, IMapper mapper)
        {
            _userAdminRepository = userAdminRepository;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var userAdmins = _mapper.Map<List<UserAdminDto>>(await _userAdminRepository.GetAllUserAdmins());
            return View(userAdmins);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userAdmin = _mapper.Map<UserAdminDto>(await _userAdminRepository.GetUserAdminById(id));
            return View(userAdmin);
        }

        public async Task<IActionResult> Create()
        {
            UserAdminDto userAdmin = new UserAdminDto();
            var users = await _userAdminRepository.GetAllUsers();
            List<SelectListItem> usersList = new List<SelectListItem>();
            foreach(var item in users)
            {
                if(await _userManager.IsInRoleAsync(item, "User"))
                {
                    usersList.Add(new SelectListItem()
                    {
                        Value = item.UserName,
                        Text = item.UserName
                    });
                }
            }
            userAdmin.UsersList = usersList;
            userAdmin.AllTasks = await _userAdminRepository.GetAllTasks();
            return View(userAdmin);
        }

        [HttpPost]
        public IActionResult Create(UserAdminDto userAdminDto)
        {
            var userAdmin = _mapper.Map<UserAdmin>(userAdminDto);

            _userAdminRepository.Create(userAdmin);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var userAdmin = _mapper.Map<UserAdminDto>(await _userAdminRepository.GetUserAdminById(id));
            var users = await _userAdminRepository.GetAllUsers();
            List<SelectListItem> usersList = new List<SelectListItem>();
            foreach (var item in users)
            {
                if(await _userManager.IsInRoleAsync(item, "User"))
                {
                    usersList.Add(new SelectListItem()
                    {
                        Value = item.UserName,
                        Text = item.UserName
                    });
                }
            }
            userAdmin.UsersList = usersList;
            userAdmin.AllTasks = await _userAdminRepository.GetAllTasks();
            return View(userAdmin);
        }

        [HttpPost]
        public IActionResult Update(UserAdminDto userAdminDto)
        {
            var userAdmin = _mapper.Map<UserAdmin>(userAdminDto);

            _userAdminRepository.Update(userAdmin);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userAdmin = _mapper.Map<UserAdminDto>(await _userAdminRepository.GetUserAdminById(id));
            var users = await _userAdminRepository.GetAllUsers();
            List<SelectListItem> usersList = new List<SelectListItem>();
            foreach (var item in users)
            {
                if(await _userManager.IsInRoleAsync(item, "User"))
                {
                    usersList.Add(new SelectListItem()
                    {
                        Value = item.UserName,
                        Text = item.UserName
                    });
                }
            }
            userAdmin.UsersList = usersList;
            userAdmin.AllTasks = await _userAdminRepository.GetAllTasks();
            return View("Delete", userAdmin);
        }

        [HttpPost]
        public IActionResult DeleteUserAdmin(int id)
        {
            _userAdminRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
