using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EclassCDCD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EclassCDCD.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserProfileController : ControllerBase
	{
		private UserManager<ApplicationUser> _userManager;
		private readonly CoreDbContext _context;
		public UserProfileController(UserManager<ApplicationUser> userManager, CoreDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}
		[HttpGet]
		//("{EmployeeId}")
		[Authorize]
		public async Task<IActionResult> GetUserProfile()
		//public async Task<IActionResult> GetUserProfile(string EmployeeId)
		{
			string EmployeeId = User.Claims.First(c => c.Type == "EmployeeId").Value;
			//var user = await _userManager.FindByIdAsync(employeeId);
			var user = await _context.Employees.FindAsync(EmployeeId);
			//var user = await _userManager.FindByIdAsync(EmployeeID);
			//user.setRequestProperty("Content-Type", "application/json");
			return Ok(new
			{
				employeeId = user.EmployeeId,
				password = user.Password,
				fullName = user.FullName,
				gender = user.Gender.ToString(),
				birthday = user.Birthday,
				address = user.Address,
				email = user.Email,
				phoneNumber = user.PhoneNumber,
				photo=user.Photo,
				departmentId = user.DepartmentId,
			});
		}
	}
}
