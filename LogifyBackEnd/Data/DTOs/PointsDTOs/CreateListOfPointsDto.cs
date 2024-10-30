namespace LogifyBackEnd.Data.DTOs.PointsDTOs;

public class CreateListOfPointsDto
{
    public List<CreatePointDto> Points { get; set; } = new List<CreatePointDto>();
}