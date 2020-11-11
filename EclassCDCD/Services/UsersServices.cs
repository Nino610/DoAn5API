using EclassCDCD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EclassCDCD.Services
{
	public interface IUserServices
	{
		Employees Authenticate(string Username, string Password);
		IEnumerable<Accounts> GetAll();
		Accounts GetById(Guid id);
		Accounts Create(Accounts Username, string Password);
		void Update(Accounts UserName, string Password);
		void Delete(Guid id);
	}

	public class UsersServices
	{
	
	}
}
