﻿using BisleriumProject.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace BisleriumProject.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
