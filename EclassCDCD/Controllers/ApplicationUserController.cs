using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EclassCDCD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EclassCDCD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private UserManager<Accounts> _userManager1;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSetting _appSetting;
        private readonly CoreDbContext _context;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, CoreDbContext context, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSetting> appSetting)
        {
            _userManager = userManager;
            //_userManager1 = userManager;
            _signInManager = signInManager;
            _appSetting = appSetting.Value;
            _context = context;
        }
        // [HttpPost]
        //[Route("register")]
        //public async Task<object> PostApplicationUser(ApplicationUser model)
        //{
        //var applicationuser = new ApplicationUser()
        //{
        //EmployeeID = model.EmployeeID,
        //Password = model.Password,
        //Role = model.Role,
        //  DepartmentId = model.DepartmentId,
        //};
        // try
        //{
        //var result = await _userManager.CreateAsync(applicationuser, model.Password);
        //  return Ok(result);
        //}
        //catch(Exception ex)
        //{
        //  throw ex;
        //}            
        //}
        [HttpPost]
        [Route("login")]
        //post
        public async Task<IActionResult> Login(Accounts account)
        {
            var user1 = await _userManager.FindByNameAsync(account.Username);
            var user = await _context.Accounts.Where(x => x.Username == account.Username && x.Password == account.Password).SingleOrDefaultAsync();
            if (user != null)
            {
                //var role = await _userManager.GetRolesAsync(user1);
                var role = await _userManager1.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Username", user.Username.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    //Expires = DateTime.UtcNow.AddMinutes(5),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new
                {
                    employeeId = user.Username,
                    password = user.Password,
                    role = user.Role,
                  
                    departmentId = user.DepartmentId,
                    token
                });
            }
            else
                return BadRequest(new { Message = "mật khẩu sai" });
        }
    }
}
