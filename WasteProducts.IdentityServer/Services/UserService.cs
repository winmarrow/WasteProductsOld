﻿using IdentityServer3.AspNetIdentity;
using WasteProducts.IdentityServer.Managers;
using WasteProducts.IdentityServer.Models;

namespace WasteProducts.IdentityServer.Services
{
    public class UserService : AspNetIdentityUserService<User, string>
    {
        public UserService(UserManager userManager)
            : base(userManager)
        {
        }
    }
}