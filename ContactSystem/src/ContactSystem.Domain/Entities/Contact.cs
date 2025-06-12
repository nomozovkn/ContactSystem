namespace ContactSystem.Domain.Entities;

public class Contact
{
    public long ContactId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } // Navigation property to User entity
}
