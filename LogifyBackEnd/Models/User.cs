namespace LogifyBackEnd.Models;

public class User
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string Surname { get; set; }
    public int PhoneNumber { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
}
