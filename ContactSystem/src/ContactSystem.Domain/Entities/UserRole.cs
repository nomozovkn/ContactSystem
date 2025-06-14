using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Domain.Entities;

public class UserRole
{
    [Key]
    public long RoleId { get; set; }
    public string RoleName { get; set; } // e.g., "Admin", "User", etc.
    public string Description { get; set; } // Optional description of the role
    public ICollection<User> Users { get; set; } // Navigation property to User entity
   

}
