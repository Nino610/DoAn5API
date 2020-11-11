using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EclassCDCD.Models
{
	public class ApplicationUser:IdentityUser
	{
        //public int Id { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string Role { get; set; }
       
        //[StringLength(10)]
        public string EmployeeId { get; set; }
        //[StringLength(150)]
        public string FullName { get; set; }
        //[StringLength(10)]
        public bool Gender { get; set; }
        //[StringLength(10)]
        public string Birthday { get; set; }
       // [StringLength(250)]
        public string Address { get; set; }
        //[StringLength(50)]
        public string Email { get; set; }
        //[StringLength(50)]
        public string PhoneNumber { get; set; }
      //  [StringLength(50)]
        public string Password { get; set; }
       // [StringLength(250)]
        public string Photo { get; set; }
       
        //[JsonIgnore]
        //[StringLength(10)]
        public string DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        //[InverseProperty(nameof(Departments.Accounts))]
        public virtual Departments Department { get; set; }
    }
}
