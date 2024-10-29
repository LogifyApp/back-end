using LogifyBackEnd.Data;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;

namespace LogifyBackEnd.Services;

public class DriverService : IDriverService
{
    private readonly DBContext _context;

    public DriverService(DBContext context)
    {
        _context = context;
    }

    // a. Accept request from employer
    public async Task<bool> AcceptRequest(int employerId, int driverId)
    {
        var driver = await _context.Drivers.FindAsync(driverId);

        if (driver == null || driver.Status == DriverStatus.Pending)
            return false;

        driver.Status = DriverStatus.Ready;
        await _context.SaveChangesAsync();
        return true;
    }
}