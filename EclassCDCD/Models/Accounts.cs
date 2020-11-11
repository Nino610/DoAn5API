using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace EclassCDCD.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            Permissions = new HashSet<Permissions>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        [Column("DepartmentID")]
        [StringLength(10)]
        public string DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty(nameof(Departments.Accounts))]
        public virtual Departments Department { get; set; }
        [InverseProperty("UsernameNavigation")]
        public virtual ICollection<Permissions> Permissions { get; set; }

		internal void setRequestProperty(string v1, string v2)
		{
			throw new NotImplementedException();
		}
	}
}
