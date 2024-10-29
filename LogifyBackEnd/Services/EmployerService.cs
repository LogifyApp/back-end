using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class EmployerService : IEmployerService
{
    private readonly DBContext _context;

    public EmployerService(DBContext context)
    {
        _context = context;
    }

    // a. Get list of drivers for an employer
    public async Task<List<Driver>> GetListOfDrivers(int employerId)
    {
        return await _context.Drivers
            .Where(d => d.UserId == employerId)
            .Include(d => d.User)
            .ToListAsync();
    }

    // b. Send request to driver
    public async Task<bool> SendRequestToDriver(int employerId, string driverPhoneNumber)
    {
        var driver = await _context.Drivers.Include(d => d.User)
            .FirstOrDefaultAsync(d => d.User.PhoneNumber == driverPhoneNumber && d.Status == DriverStatus.WithoutEmp);

        if (driver == null)
            return false;

        driver.Status = DriverStatus.Pending;
        await _context.SaveChangesAsync();
        return true;
    }

    // c. Soft delete driver by adding end date in EmployerDriverHistory
    public async Task<bool> SoftDeleteDriver(int employerId, int driverId)
    {
        var history = await _context.EmployerDriverHistories
            .FirstOrDefaultAsync(h => h.EmployerUserId == employerId && h.DriverUserId == driverId && h.EndDate == null);

        if (history == null)
            return false;

        history.EndDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }
}