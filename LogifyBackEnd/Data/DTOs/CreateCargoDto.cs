using System.ComponentModel.DataAnnotations;

namespace LogifyBackEnd.Data.DTOs;

public class CreateCargoDto
{
    [Required]
    public string Status { get; set; }
    public string Description { get; set; }
    public string CarId { get; set; }
    [Required]
    public int DriverUserId { get; set; }
    [Required]
    public int EmployerUserId { get; set; }
}