using Microsoft.AspNetCore.Mvc.Formatters;
using OngProject.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.DataAccess.Seeds
{
    public static class RoleSeed
    {
        public static IEnumerable<Role> GetData()
        {
            var roles = new[] {
                new Role() { Id = 1, Name = "Admin",Description = "admins"},
                new Role() { Id = 2, Name = "User",Description = "users"},
            };

            foreach (var role in roles)
            {
                role.CreatedAt = role.LastEditedAt = DateTime.UtcNow;
            }
            return roles;

        }

    }
}
