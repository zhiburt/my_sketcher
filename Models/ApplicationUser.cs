using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace lastVersionAuthe.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public String Manuals { get; set; }

        public String Name { get; set; }

        public String GitHubUrl { get; set; }

        public String VkontakteUrl { get; set; }

        public String TwitterUrl { get; set; }
    }
}
