using System.ComponentModel.DataAnnotations;

namespace LogifyBackEnd.Models;

public class Employer
{
    [Key]
    public int UserId { get; set; } // Foreign Key to User
    public User User { get; set; }
}
