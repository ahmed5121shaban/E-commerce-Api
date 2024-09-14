using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Manager
{
    public  class AcountManager
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }

        public AcountManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }



        public async Task<SignInResult> Login(UserLoginViewModel user)
        {

            var u = await UserManager.FindByEmailAsync(user.Login);

            if (u == null)
                u = await UserManager.FindByNameAsync(user.Login);

            return await SignInManager.PasswordSignInAsync(
                    u,
                    user.Password,
                    user.RemeberMe,
                    true
            );
        }

        public async Task<IdentityResult> Register(UserRegisterViewModel user)
        {
            var u = user.MapFromRegisterToUser();
            var res = await UserManager.CreateAsync(u, user.Password);
            await UserManager.AddToRoleAsync(u, "Admin");
            return res;

        }

        public async void LogOut()
        {
            await SignInManager.SignOutAsync();
        }

    }
}
