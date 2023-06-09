﻿using BusinessLogic.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace BusinessLogic.Interfaces
{
    public interface IAppUserService
    {
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        string CreateToken(IEnumerable<Claim> claims);
        Task<AppUser> FindByIdAsync(string currentUserId);
        Task<AppUser> FindByNameAsync(string userName);
        Task<(string, string, bool)> LoginAsync(AppUser user, string password);
        Task<IEnumerable<AppUser>> Get();
    }
}
