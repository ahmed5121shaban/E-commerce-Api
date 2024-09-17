using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ViewModel;

namespace E_commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        AcountManager AcountManager { get; set; }

        public AcountController(AcountManager acountManager)
        {
            AcountManager = acountManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index", "home");
            }
            var res = await AcountManager.Login(user);
            if (res.Succeeded)
            {
                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError("", "The Email/User Name or Password is not Valid");
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index", "home");
            }

            var res = await AcountManager.Register(user);
            if (res.Succeeded)
            {
                return RedirectToAction("login", "acount");
            }
            else
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return RedirectToAction("index", "home");
            }


        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePassword viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var res = await AcountManager.ChangePassword(viewmodel);
                if (res.Succeeded)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return RedirectToAction("index", "home");
                }
            }
            return RedirectToAction("index", "home");

        }

        //To get the email and check it from client
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var code = await AcountManager.GetResetPasswordCode(email);
            if (string.IsNullOrEmpty(code))
            {
                ModelState.AddModelError("", "Your Email Is Not Here");
                return RedirectToAction("");
            }
            else
            {
                EmailHelper mail = new EmailHelper
                    (email, "Your Code is", $" Code :  {code}");
                mail.Send();
                return RedirectToAction("");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RessetPassword(UserResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = await AcountManager.RessetPassword(viewModel);
                if (res.Succeeded)
                {
                    return RedirectToAction("login", "Acount");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return RedirectToAction("login", "Acount");
        }
    }
}
