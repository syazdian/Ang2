using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNymti.Models.Entities
{
    public class AppUser : IdentityUser
    {
        // extended properties
        public string firstname { get; set; }

        public string lastname { get; set; }
       
    }
}
