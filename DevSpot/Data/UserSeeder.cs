﻿using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data;

public class UserSeeder
{
    public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        await CreateUserWithRole(userManager, "admin@devspot.com", "Admin123@", Roles.Admin);
        await CreateUserWithRole(userManager, "jobseeker@devspot.com", "Jobseeker123@", Roles.JobSeeker);
        await CreateUserWithRole(userManager, "employee@devspot.com", "Employee123@", Roles.Employer);


    }
    private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email,
        string password, string role)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new IdentityUser
            {
                Email = email,
                EmailConfirmed = true,
                UserName = email,
            };
            
            var res = await userManager.CreateAsync(user, password);
            
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",",res.Errors)}");
            }
        }    
    }
}