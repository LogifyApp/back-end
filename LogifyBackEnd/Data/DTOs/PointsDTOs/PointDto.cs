namespace LogifyBackEnd.Data.DTOs.PointsDTOs;

public class PointDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public int Latitude { get; set; }
    public int Longitude { get; set; }
    public int Order { get; set; }
    public int CargoId { get; set; }
}