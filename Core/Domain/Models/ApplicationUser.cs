﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }=true;
        public string? ProfilePictureUrl { get; set; }
    }
}
