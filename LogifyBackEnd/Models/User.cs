using System.ComponentModel.DataAnnotations;

namespace LogifyBackEnd.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    public string Role { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
}
