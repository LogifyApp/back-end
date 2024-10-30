using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class CarService(DBContext context) : ICarService
{
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

        context.Cars.Add(car);
        await context.SaveChangesAsync();
        return car;
    }

    public async Task<Car?> UpdateCar(string carNumber, string? model, string? brand, bool status)
    {
        var car = await context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber);
        if (car == null || car.IsDeleted)
        {
            return null;
        }

        car.Model = model ?? car.Model;
        car.Brand = brand ?? car.Brand;
        car.Status = status;
        await context.SaveChangesAsync();

        return car;
    }

    public async Task<Car?> SoftDeleteCar(string carNumber)
    {
        var car = await context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber);
        if (car == null)
        {
            return null;
        }

        car.IsDeleted = true;
        await context.SaveChangesAsync();

        return car;
    }

    public async Task<Car?> GetCar(string carNumber)
    {
        return await context.Cars.FirstOrDefaultAsync(c => c.Number == carNumber && !c.IsDeleted);
    }

    public async Task<List<Car>> GetListOfCars(int employerId)
    {
        return await context.Cars
            .Where(c => c.EmployerUserId == employerId && !c.IsDeleted)
            .ToListAsync();
    }
}