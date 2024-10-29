namespace LogifyBackEnd.Services.Interfaces;

public interface IDriverService
{
    Task<bool> AcceptRequest(int employerId, int driverId);
}