using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Salt { get; set; }
    public long RoleId { get; set; } // Foreign key to UserRole
    public UserRole Role { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }/* = new List<RefreshToken>();*/
    public ICollection<Contact> Contacts { get; set; } /*= new List<Contact>();*/
}
