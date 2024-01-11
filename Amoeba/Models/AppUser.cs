﻿using Microsoft.AspNetCore.Identity;

namespace Amoeba.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

    }
}
