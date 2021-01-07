using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
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
			string userId = User.Claims.First(c => c.Type == "Username").Value;
			//var user = await _userManager.FindByIdAsync(employeeId);
			var user = await _context.Employees.FindAsync(userId);
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
		[HttpGet]
		[Authorize(Roles ="1")]
		[Route("ForCanBo")]
		public string GetForCanBo()
		{
			return "đây là cán bộ";
		}

		[HttpGet]
		[Authorize(Roles = "2")]
		[Route("ForGiangVien")]
		public string GetForGiangVien()
		{
			return "đây là giảng viên";
		}

		[HttpGet]
		[Authorize(Roles = "3")]
		[Route("ForSinhVien")]
		public string GetForSinhVien()
		{
			return "đây là sinh viên";
		}

		[HttpPost,DisableRequestSizeLimit]
		[Route("upload")]
		public IActionResult Upload()
		{
			try
			{
				var file = Request.Form.Files[0];
				var folderName = Path.Combine("Resources","Images");
				var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);
				if(file.Length>0)
				{
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					var fullPath = Path.Combine(pathToSave, fileName);
					var dbPath = Path.Combine(folderName, fileName);
					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);

					}
					return Ok(new { dbPath });
				}
				else
				{
					return BadRequest();
				}	
			}
			catch(Exception ex)
			{
				return StatusCode(500, $"lỗi:{ex}");
			}
		}
	}
}
