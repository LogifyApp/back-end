using LogifyBackEnd.Data;

namespace LogifyBackEnd.Services;

public class CarService
{
    private readonly DBContext _context;

    public CarService(DBContext context)
    {
        _context = context;
    }

    public async Task SoftDeleteCar(string carNumber)
    {
        var car = await _context.Cars.FindAsync(carNumber);
        if (car != null)
        {
            car.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
