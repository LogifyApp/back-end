using System.ComponentModel.DataAnnotations;

namespace LogifyBackEnd.Data.DTOs.MiddlewareDTOs;

public class RegisterDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Required]
    public string Role { get; set; }
}
