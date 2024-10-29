using System.ComponentModel.DataAnnotations;
using LogifyBackEnd.Models.Enums;

namespace LogifyBackEnd.Models;

public class Driver
{
    [Key]
    public int UserId { get; set; } // Foreign Key to User
    public DriverStatus Status { get; set; }
    public User User { get; set; }
}
