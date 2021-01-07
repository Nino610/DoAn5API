using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EclassCDCD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.VisualBasic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace EclassCDCD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private UserManager<Accounts> _userManager1;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly CoreDbContext _context;
        private readonly ApplicationSetting _appSetting;
        public AccountsController(CoreDbContext context, IOptions<ApplicationSetting> appSetting)
        {
            _context = context;
            _appSetting = appSetting.Value;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accounts>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccounts(string id)
        {
            var accounts = await _context.Accounts.FindAsync(id);

            if (accounts == null)
            {
                return NotFound();
            }

            return accounts;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccounts(string id, Accounts accounts)
        {
            if (id != accounts.Username)
            {
                return BadRequest();
            }

            _context.Entry(accounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("them")]
        public async Task<ActionResult<Accounts>> PostAccounts(Accounts accounts)
        {
            _context.Accounts.Add(accounts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountsExists(accounts.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccounts", new { id = accounts.Username }, accounts);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Accounts>> DeleteAccounts(string id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();

            return accounts;
        }

        private bool AccountsExists(string id)
        {
            return _context.Accounts.Any(e => e.Username == id);
        }
        [HttpPost]
        [Route("login")]
        //post
        public async Task<IActionResult> Login( Accounts account)
        {
            //var user1 = await _userManager.FindByIdAsync(account.Username);
            var user = await _context.Accounts.Where(x => x.Username == account.Username && x.Password == account.Password).SingleOrDefaultAsync();
            if (user != null)
            {
               // var role = await _userManager.GetRolesAsync(user1);
               // var role = await _userManager1.GetRolesAsync(user);
                var role = user.Role;
                IdentityOptions _options = new IdentityOptions();
               
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Username", user.Username.ToString()),
                         new Claim(ClaimTypes.Role, user.Role),
                //new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
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
                    userName = user.Username,
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
