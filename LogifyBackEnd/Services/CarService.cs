using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class CarService : ICarService
{
    private readonly DBContext _context;

    public CarService(DBContext context)
    {
        _context = context;
    }

    public async Task<Car> AddCar(int employerId, string number, string? model = null, string? brand = null)
    {
        var car = new Car
        {
            Number = number,
            Model = model,
            Brand = brand,
            Status = true,
            IsDeleted = false,
            EmployerUserId = employerId
        };

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task<Car?> UpdateCar(string carNumber, string? model, string? brand, bool status)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber);
        if (car == null || car.IsDeleted)
        {
            return null;
        }

        car.Model = model ?? car.Model;
        car.Brand = brand ?? car.Brand;
        car.Status = status;
        await _context.SaveChangesAsync();

        return car;
    }

    public async Task<Car?> SoftDeleteCar(string carNumber)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber);
        if (car == null)
        {
            return null;
        }

        car.IsDeleted = true;
        await _context.SaveChangesAsync();

        return car;
    }

    public async Task<Car?> GetCar(string carNumber)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber && !c.IsDeleted);
    }

    public async Task<List<Car>> GetListOfCars(int employerId)
    {
        return await _context.Cars
            .Where(c => c.EmployerUserId == employerId && !c.IsDeleted)
            .ToListAsync();
    }
}