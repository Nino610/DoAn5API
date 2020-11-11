using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EclassCDCD.Models
{
	public class ApplicationSetting:IdentityUser
	{
		public string JWT_Secret { get; set; }
		public string Client_URL { get; set; }
	}
}
