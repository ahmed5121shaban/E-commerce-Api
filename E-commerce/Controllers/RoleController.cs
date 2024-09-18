﻿using Manager;
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

        [HttpPost]
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

        [HttpPost]
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


    }
}
