using LogifyBackEnd.Models.Enums;

namespace LogifyBackEnd.Data.DTOs;

public class CargoCreateDto
{
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public string CarId { get; set; } = string.Empty;
    public int DriverUserId { get; set; } = 0;
    public int EmployerUserId { get; set; } = 0;
}
