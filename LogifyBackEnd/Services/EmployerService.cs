using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogifyBackEnd.Services;

public class EmployerService(DBContext context) : IEmployerService
{
    public async Task<List<Driver>> GetListOfDrivers(int employerId)
    {
        return await context.Drivers
            .Where(d => d.UserId == employerId)
            .Include(d => d.User)
            .ToListAsync();
    }

    public async Task<bool> SendRequestToDriver(int employerId, string driverPhoneNumber)
    {
        var employerExists = await context.Employers.AnyAsync(e => e.UserId == employerId);
        if (!employerExists)
        {
            // Log or handle the case where the employer does not exist
            return false;
        }

        var driver = await context.Drivers
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.User.PhoneNumber == driverPhoneNumber && d.Status == DriverStatus.WithoutEmp);

        if (driver == null)
        {
            // Log or handle the case where the driver is not found or not available
            return false;
        }

        driver.Status = DriverStatus.Pending;
    
        await context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> SoftDeleteDriver(int employerId, int driverId)
    {
        var history = await context.EmployerDriverHistories
            .FirstOrDefaultAsync(h => h.EmployerUserId == employerId && h.DriverUserId == driverId && h.EndDate == null);

        if (history == null)
            return false;

        history.EndDate = DateTime.Now;
        await context.SaveChangesAsync();
        return true;
    }
}