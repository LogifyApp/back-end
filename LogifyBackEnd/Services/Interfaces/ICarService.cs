using LogifyBackEnd.Models;

namespace LogifyBackEnd.Services.Interfaces;

public interface ICarService
{
    Task<Car> AddCar(int employerId, string number, string? model = null, string? brand = null);
    Task<Car?> UpdateCar(string carNumber, string? model, string? brand, bool status);
    Task<Car?> SoftDeleteCar(string carNumber);
    Task<Car?> GetCar(string carNumber);
    Task<List<Car>> GetListOfCars(int employerId);
}