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
        public ILogger<RoleController> Logger { get; }

        public RoleController(RoleManager _roleManager, UserManager<User> _userManager,ILogger<RoleController> _logger)
        {
            roleManager = _roleManager;
            UserManager = _userManager;
            Logger = _logger;
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(e=>e.Errors).Select(e=>e.ErrorMessage).ToList();
                return BadRequest(error);
            }

            var res = await roleManager.Add(role);
            if (!res.Succeeded)
            {
                return BadRequest(new {Massage = "You Have Error In Your Addding" });
            }
            return Ok();
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(string id, string role)
        {
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            var res = await UserManager.AddToRoleAsync(user, role);
            if (res.Succeeded)
            {
                return Ok();
            }
            return BadRequest(res.Errors.Select(e=>e.Description));
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles() 
        {
            var roles = roleManager.GetAll().ToList();
            if (!roles.Any()) { return NoContent(); }
            return Ok(roles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Roles(string id)
        {
            var role =await roleManager.GetByID(id);
            if (role == null) { return BadRequest(new { Massage = "No role With This ID" }); }
            if (roleManager.Delete(role)){ return NoContent(); }
            return BadRequest(new { Massage = "Not Deleted" });
        }

    }
}
