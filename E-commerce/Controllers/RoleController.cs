using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModel;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager roleManager;
        public UserManager<User> UserManager { get; }
        public RoleController(RoleManager _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            UserManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index", "home");
            }

            var res = await roleManager.Add(role);
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "You Have Error In Your Addding");
            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string id, string role)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            var res = await UserManager.AddToRoleAsync(user, role);
            if (res.Succeeded)
            {
                return RedirectToAction("index", "home");
            }
            return RedirectToAction("index", "home");
        }


    }
}
