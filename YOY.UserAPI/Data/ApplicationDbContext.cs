using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.v1.IdentityModel.AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}