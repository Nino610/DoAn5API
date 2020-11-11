using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EclassCDCD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EclassCDCD.Models
{
	public class AuthenticationContext:IdentityDbContext
	{
		public AuthenticationContext(DbContextOptions options):base(options)
		{ }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
