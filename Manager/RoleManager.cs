using Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Manager
{
    public class RoleManager : BaseManager<IdentityRole>
    {

            public RoleManager<IdentityRole> roleManager { get; set; }
            public RoleManager(RoleManager<IdentityRole> _roleManager, AppDbContext myDB) : base(myDB)
            {
                roleManager = _roleManager;
            }
            public async Task<IdentityResult> Add(RoleViewModel roleViewModel)
                {
                    return await roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.Name });
                }

        
    }
}
