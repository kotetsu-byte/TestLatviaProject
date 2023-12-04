using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using TestLatviaProject.Models;
using TestLatviaProject.View_Models;

namespace TestLatviaProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Register()
        {
            RegisterVM registerVM = new RegisterVM();
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Manager"));
            await _roleManager.CreateAsync(new IdentityRole("User"));
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            selectListItems.Add(new SelectListItem()
            {
                Value = "Manager",
                Text = "Manager"
            });
            selectListItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });
            registerVM.RoleList = selectListItems;
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var user = new User { Name = registerVM.Name, UserName = registerVM.UserName, Email = registerVM.Email };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                if(registerVM.RoleSelected != null && registerVM.RoleSelected.Length > 0)
                {
                    await _userManager.AddToRoleAsync(user, registerVM.RoleSelected);
                } else
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login");
            }

            return View(registerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
