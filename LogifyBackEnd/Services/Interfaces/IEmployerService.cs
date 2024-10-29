using LogifyBackEnd.Models;

namespace LogifyBackEnd.Services.Interfaces;

public interface IEmployerService
{
    Task<List<Driver>> GetListOfDrivers(int employerId);
    Task<bool> SendRequestToDriver(int employerId, string driverPhoneNumber);
    Task<bool> SoftDeleteDriver(int employerId, int driverId);
}