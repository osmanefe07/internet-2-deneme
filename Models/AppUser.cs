﻿using Microsoft.AspNetCore.Identity;

namespace vize.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
