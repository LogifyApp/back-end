namespace LogifyBackEnd.Models;

public class CurrentLocation
{
    public int DriverId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
