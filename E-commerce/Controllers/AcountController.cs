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
        private readonly ILogger<AcountController> logger;
        private readonly TokenManager tokenManager;
        private readonly IConfiguration configuration;

        AcountManager AcountManager { get; set; }

        public AcountController(AcountManager acountManager,ILogger<AcountController> _logger,
            TokenManager tokenManager,IConfiguration configuration)
        {
            AcountManager = acountManager;
            logger = _logger;
            this.tokenManager = tokenManager;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                return BadRequest(error);
            }
            var res = await AcountManager.Login(user);
            if (res.Succeeded)
            {
                var _token =tokenManager.GenerateToken(user.MapFromLoginToUser());
                return Ok(new {token= _token,expiration = DateTime.Now.AddDays(1) });
            }

            return Unauthorized(new { Message = "Invalid email/username or password" });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage)
                           .ToList();
                return BadRequest(error);
            }

            var res = await AcountManager.Register(user);
            if (res.Succeeded)
            {
                return Ok();
            }

            return BadRequest(res.Errors.Select(e=>e.Description));
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePassword viewmodel)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                          .SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();
                return BadRequest(error);
            }
            
            viewmodel.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await AcountManager.ChangePassword(viewmodel);
            if (res.Succeeded)
              {
                 return Ok();
              }
             return BadRequest(res.Errors.Select(e=>e.Description));

        }

        //To get the email and check it from client
        [HttpGet("reset-password/{email}")]
        public async Task<IActionResult> RessetPassword(string email)
        {
            var code = await AcountManager.GetResetPasswordCode(email);
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest(new {Massage= "Your Email Is Not Here" });
            }

            EmailHelper mail = new EmailHelper(email, "Your Code is", $" Code :  {code}");
            mail.Send();
            return Ok();
            
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RessetPassword(UserResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                          .SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();
                return BadRequest(error);
            }
            var res = await AcountManager.RessetPassword(viewModel);
            if (res.Succeeded)
            {
                return Ok(new {Massage="Your Password Is Resset"});
            }
            return BadRequest(res.Errors.Select(e=>e.Description));
        }
    }
}
