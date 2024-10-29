namespace LogifyBackEnd.Data.DTOs;

public class CargoDto
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public string CarId { get; set; }
    public int DriverUserId { get; set; }
    public int EmployerUserId { get; set; }
        
    // List of associated points
    public List<PointDto> Points { get; set; } = new List<PointDto>();
}